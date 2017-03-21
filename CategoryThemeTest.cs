using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NUnit.Framework;
using SharpMap.Data;
using SharpMap.Styles;

namespace SharpMap.Rendering.Thematics.CategoryThemeTest
{
    public class CategoryThemeTest
    {
        [Test]
        public void TestCategoryRangeItemInt32()
        {
            // arrange
            var ri = new CategoryThemeRangeItem<int> { LowerBound = 5, UpperBound = 10, Style = VectorStyle.CreateRandomStyle()};

            // act

            // assert
            Assert.That(ri, Is.Not.Null);
            Assert.That(ri.Title, Is.EqualTo("5 - 10"));
            ri.Title = "[5, 10[";
            Assert.That(ri.Title, Is.EqualTo("[5, 10["));

            Assert.That(ri.Matches(4), Is.False);
            Assert.That(ri.Matches(5), Is.True);
            Assert.That(ri.Matches(6), Is.True);
            Assert.That(ri.Matches(9), Is.True);
            Assert.That(ri.Matches(10), Is.False);
            Assert.That(ri.Matches(11), Is.False);
        }

        [Test]
        public void TestCategoryRangeItemDouble()
        {
            // arrange
            var ri = new CategoryThemeRangeItem<double>
            {
                LowerBound = 5.01, UpperBound = 9.99,
                Style = VectorStyle.CreateRandomStyle()
            };

            // act

            // assert
            Assert.That(ri, Is.Not.Null);
            Assert.That(ri.Title, Is.EqualTo(string.Format("{0} - {1}", 5.01, 9.99)));
            ri.Title = "[5.01 - 9.99[";
            Assert.That(ri.Title, Is.EqualTo("[5.01 - 9.99["));

            Assert.That(ri.Matches(5), Is.False);
            Assert.That(ri.Matches(5.01), Is.True);
            Assert.That(ri.Matches(6), Is.True);
            Assert.That(ri.Matches(9), Is.True);
            Assert.That(ri.Matches(9.99), Is.False);
            Assert.That(ri.Matches(10), Is.False);

            Assert.That(ri.Style != null, Is.True);
        }

        [Test]
        public void TestCategoryValuesItem()
        {
            // arrange
            var vi = new CategoryThemeValuesItem<string>
            {
                Values = new List<string> { "A", "B", "C", "D"},
                Style = VectorStyle.CreateRandomStyle()
            };

            // act

            // assert
            Assert.That(vi, Is.Not.Null);
            Assert.That(vi.Title, Is.EqualTo(string.Join(",", vi.Values.ToArray())));
            vi.Title = "A, B, C or D";
            Assert.That(vi.Title, Is.EqualTo("A, B, C or D"));

            Assert.That(vi.Matches("A"), Is.True);
            Assert.That(vi.Matches("B"), Is.True);
            Assert.That(vi.Matches("C"), Is.True);
            Assert.That(vi.Matches("D"), Is.True);
            Assert.That(vi.Matches("0"), Is.False);
            Assert.That(vi.Matches("1"), Is.False);

            Assert.That(vi.Style != null, Is.True);
        }

        [Test]
        public void TestCategoryThemeWithRange()
        {
            // Arrange
            var ct = new CategoryTheme<double>();
            ct.ColumnName = "Value";
            ct.Default =  VectorStyle.CreateRandomStyle(); 

            // Add unordered
            ct.Add(new CategoryThemeRangeItem<double> { LowerBound = 0d, UpperBound = 5d, Style = VectorStyle.CreateRandomStyle()});
            ct.Add(new CategoryThemeRangeItem<double> { LowerBound = 10d, UpperBound = 15d, Style = VectorStyle.CreateRandomStyle() });
            ct.Add(new CategoryThemeRangeItem<double> { LowerBound = 5d, UpperBound = 10d, Style = VectorStyle.CreateRandomStyle() });
            ct.Add(new CategoryThemeRangeItem<double> { LowerBound = 20d, UpperBound = 25d, Style = VectorStyle.CreateRandomStyle() });

            // act & assert
            Assert.That(ct, Is.Not.Null);

            Assert.That(ct.ItemsAsReadOnly(), Is.Not.Null);
            Assert.That(ct.ItemsAsReadOnly().Count, Is.EqualTo(4));

            var fdt = CreateTable("test", ct.ColumnName, typeof(double), 1, 7, 12, 18, 24);

            DoTest(ct, fdt);

        }

        [Test]
        public void TestCategoryThemeWithValues()
        {
            // Arrange
            var ct = new CategoryTheme<string>();
            ct.ColumnName = "Value";
            ct.Default = VectorStyle.CreateRandomStyle();

            // Add unordered
            ct.Add(new CategoryThemeValuesItem<string> { Values = new List<string> { "A", "B"} , Style = VectorStyle.CreateRandomStyle() });
            ct.Add(new CategoryThemeValuesItem<string> { Values = new List<string> { "C", "D" }, Style = VectorStyle.CreateRandomStyle() });
            ct.Add(new CategoryThemeValuesItem<string> { Values = new List<string> { "E", "F" }, Style = VectorStyle.CreateRandomStyle() });
            ct.Add(new CategoryThemeValuesItem<string> { Values = new List<string> { "G", "H" }, Style = VectorStyle.CreateRandomStyle() });

            // act & assert
            Assert.That(ct, Is.Not.Null);

            Assert.That(ct.ItemsAsReadOnly(), Is.Not.Null);
            Assert.That(ct.ItemsAsReadOnly().Count, Is.EqualTo(4));

            var fdt = CreateTable("test", ct.ColumnName, typeof(string), "A", "B", "C", "D", "E", "F", "G", "H", "I");

            DoTest(ct, fdt);

        }
        private static void DoTest<T>(CategoryTheme<T> ct, FeatureDataTable fdt) where T : IComparable<T>
        {
            var ctis = ct.ItemsAsReadOnly();
            Assert.That(ctis, Is.Not.Null);

            var i = 0;
            foreach (FeatureDataRow dataRow in fdt.Rows)
            {
                i++;
                IStyle style = null;
                Assert.DoesNotThrow(() => style = ct.GetStyle(dataRow));
                if (dataRow.IsNull(ct.ColumnName))
                {
                    if (ct.UseDefaultStyleForDbNull)
                        Assert.That(style, Is.SameAs(ct.Default));
                    else
                        Assert.That(style, Is.Null);
                }
                else
                {
                    var cti = ctis.FirstOrDefault(t => t.Matches((T)Convert.ChangeType(dataRow[1], typeof(T))));
                    if (cti != null)
                        Assert.That(style, Is.SameAs(cti.Style));
                    else
                        Assert.That(style, Is.SameAs(ct.Default));
                }
            }
        }

        private static FeatureDataTable CreateTable(string tableName, string columnName, Type columnType,
            params object[] values)
        {
            var res = new FeatureDataTable();
            res.TableName = tableName;
            res.Columns.Add("FID", typeof(int));
            res.Columns.Add(columnName, columnType);
            res.PrimaryKey = new DataColumn[] { res.Columns[0]};

            res.BeginLoadData();
            var i = 1;
            for (; i < values.Length; i++)
                res.LoadDataRow(new [] {i, Convert.ChangeType(values[i], columnType)}, true);

            res.LoadDataRow(new object[] {i, DBNull.Value}, true);
            res.EndLoadData();
            return res;
        }
    }
}

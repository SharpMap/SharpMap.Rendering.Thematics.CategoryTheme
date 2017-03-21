# SharpMap.Rendering.Thematics.CategoryTheme
A categorial theme for SharpMap

## Create a category theme using value ranges
```C#
// Create a category theme that can categorize double values
var ct = new CategoryTheme<double>();
// Set the column name that holds the values to categorize
ct.ColumnName = "Value";
// Create a fallback style in case no category item matches
ct.Default =  VectorStyle.CreateRandomStyle(); 

// Add category range items. This can be done unordered
ct.Add(new CategoryThemeRangeItem<double> { LowerBound = 0d, UpperBound = 5d, Style = VectorStyle.CreateRandomStyle()});
ct.Add(new CategoryThemeRangeItem<double> { LowerBound = 10d, UpperBound = 15d, Style = VectorStyle.CreateRandomStyle() });
ct.Add(new CategoryThemeRangeItem<double> { LowerBound = 5d, UpperBound = 10d, Style = VectorStyle.CreateRandomStyle() });
ct.Add(new CategoryThemeRangeItem<double> { LowerBound = 20d, UpperBound = 25d, Style = VectorStyle.CreateRandomStyle() });
```
## Create a category theme using specific values
```C#
// Create a category theme that can categorize string values
var ct = new CategoryTheme<string>();
// Set the column name that holds the values to categorize
ct.ColumnName = "Value";
// Create a fallback style in case no category item matches
ct.Default = VectorStyle.CreateRandomStyle();

// Add category value items. These are not sorted automatically, you have to do that yourself.
ct.Add(new CategoryThemeValuesItem<string> { Values = new List<string> { "A", "B"} , Style = VectorStyle.CreateRandomStyle() });
ct.Add(new CategoryThemeValuesItem<string> { Values = new List<string> { "C", "D" }, Style = VectorStyle.CreateRandomStyle() });
ct.Add(new CategoryThemeValuesItem<string> { Values = new List<string> { "E", "F" }, Style = VectorStyle.CreateRandomStyle() });
ct.Add(new CategoryThemeValuesItem<string> { Values = new List<string> { "G", "H" }, Style = VectorStyle.CreateRandomStyle() });

```


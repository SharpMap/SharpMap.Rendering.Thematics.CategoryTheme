// Copyright 2014 - Felix Obermaier (www.ivv-aachen.de)
//
// This file is part of SharpMap.Rendering.Thematics.CategoryTheme.
// SharpMap.Rendering.Decoration.Legend is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap.Rendering.Thematics.CategoryTheme is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using SharpMap.Styles;

namespace SharpMap.Rendering.Thematics
{

	/// <summary>
	/// Abstract base class for <see cref="ICategoryThemeItem{T}"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public abstract class CategoryThemeItem<T> : ICategoryThemeItem<T>
		where T : IComparable<T>
	{
		private string _title;
		private IStyle _style;
		
		
		/// <summary>
		/// Compares the current object with another of type <see cref="ICategoryThemeItem{T}"/>.
		/// </summary>
		/// <returns>
		/// A value, that indicates the relative order of the compared objects. The value has the following meaning:
		/// <list type="Table">
		/// <item>&lt; 0</item><description>This object is less than the <paramref name="other"/>.</description>
		/// <item>0</item><description>The object is equal</description>
		/// <item>&gt;</item><description>This object is greater than the <paramref name="other"/>.</description>
		/// </list>
		/// Wert Bedeutung Kleiner als 0 (null) Dieses Objekt ist kleiner als der <paramref name="other"/>-Parameter.Zero Dieses Objekt ist gleich <paramref name="other"/>. Größer als 0 (null) Dieses Objekt ist größer als <paramref name="other"/>.
		/// </returns>
		/// <param name="other">An Object of type <see cref="ICategoryThemeItem{T}"/> that should be compared with this item.</param>
		public abstract int CompareTo(ICategoryThemeItem<T> other);
		
		/// <summary>
		/// Evaluate that <paramref name="value"/> matches this <see cref="ICategoryThemeItem{T}"/>
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <returns>true, if <paramref name="value"/></returns>
		public abstract bool Matches(T value);
		
		/// <summary>
		/// Gets or sets the title for this <see cref="CategoryThemeItem{T}"/>
		/// </summary>
		public string Title
		{
			get
			{
				return _title ?? ToString();
			}
			set
			{
				if (value.Equals(_title))
					return;
				_title = value;
				//this.OnPropertyChanged("Title");
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="IStyle"/> for this <see cref="CategoryThemeItem{T}"/>
		/// </summary>
		public IStyle Style
		{
			get
			{
				return _style;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				
				if (value.Equals(_style))
					return;
				
				_style = value;
				//this.OnPropertyChanged("Style");
			}
		}
		
	}
}

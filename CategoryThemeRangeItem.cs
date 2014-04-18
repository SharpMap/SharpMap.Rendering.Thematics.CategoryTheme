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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using SharpMap.Data;
using SharpMap.Styles;
using SharpMap.Utilities;

namespace SharpMap.Rendering.Thematics
{
	/// <summary>
	/// Implementation that
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class CategoryThemeRangeItem<T> : CategoryThemeItem<T>
		where T : IComparable<T>
	{
		private T _lowerBound;
		private T _upperBound;
		
		///<summary>
		/// Gets or sets the lower bound for this <see cref="CategoryThemeRangeItem{T}"/>
		///</summary>
		public T LowerBound
		{
			get
			{
				return this._lowerBound;
			}
			set
			{
				if (_lowerBound.CompareTo(value) == 0)
					return;
				this._lowerBound = value;
				//this.OnPropertyChanged("LowerBound");
			}
		}
		
		
		///<summary>
		/// Gets or sets the upper bound for this <see cref="CategoryThemeRangeItem{T}"/>
		///</summary>
		public T UpperBound
		{
			get
			{
				return this._upperBound;
			}
			set
			{
				if (_upperBound.CompareTo(value) == 0)
					return;
				this._upperBound = value;
				//this.OnPropertyChanged("UpperBound");
			}
		}
		
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
		/// </returns>
		/// <param name="other">An Object of type <see cref="ICategoryThemeItem{T}"/> that should be compared with this item.</param>
		public override int CompareTo(ICategoryThemeItem<T> other)
		{
			var o = other as CategoryThemeRangeItem<T>;
			if (o == null) return -1;
			
			if (this.Matches(o.LowerBound)) return 0;
			if (this.Matches(o.UpperBound)) return 0;
			
			return _lowerBound.CompareTo(o.LowerBound);
		}
		
		
		/// <summary>
		/// Evaluate that <paramref name="value"/> matches this <see cref="ICategoryThemeItem{T}"/>
		/// </summary>
		/// <remarks>
		/// <paramref name="value"/> matches if it is in the range [LowerBound, UpperBound[
		/// </remarks>
		/// <param name="value">The value to test.</param>
		/// <returns>true, if <paramref name="value"/> matches.</returns>
		public override bool Matches(T value)
		{
			if (_lowerBound.CompareTo(value) < 0) return false;
			if (_upperBound.CompareTo(value) >= 0) return false;
			return true;
		}
		
		public override string ToString()
		{
			return string.Format("{0} - {1}", _lowerBound, _upperBound);
		}
	}
}

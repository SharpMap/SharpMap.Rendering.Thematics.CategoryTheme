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
	///<summary>
	/// Interface for an item in a <see cref="CategoryTheme{T}"/>.
	///</summary>
	///<typeparam name="T">The Type of the value to compare</typeparam>
	public interface ICategoryThemeItem<T> : IComparable<ICategoryThemeItem<T>>
		where T: IComparable<T>
	{
		/// <summary>
		/// Evaluate that <paramref name="value"/> matches this <see cref="ICategoryThemeItem{T}"/>
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <returns>true, if <paramref name="value"/></returns>
		bool Matches(T value);
		
		/// <summary>
		/// Gets the title for this <see cref="ICategoryThemeItem{T}"/>
		/// </summary>
		string Title { get; }
		
		///<summary>
		/// Gets the style applicable for this item
		///</summary>
		IStyle Style { get; }
	}
}

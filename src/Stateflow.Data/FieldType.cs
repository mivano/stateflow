using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	/// <summary>
	/// Describes the available field types
	/// </summary>
	public enum FieldType{
		Unknown = 0,
		SingleLineText,
		MultiLineText,
		Amount,
		Decimal,
		Number,
		HtmlText,
		Boolean,
		SingleSelect,
		MultiSelect,
		Date,
		DateTime,
		Time,
		File,
		Label,
		Color,
		EmailAddress,
		Password,
		Url,
		List,
		Reference,
		MultiReference
	}

}

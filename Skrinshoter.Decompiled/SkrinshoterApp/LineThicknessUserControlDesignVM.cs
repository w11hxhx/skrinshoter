using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;

namespace SkrinshoterApp;

public class LineThicknessUserControlDesignVM : LocalizableObservableObject
{
	[ObservableProperty]
	private double selectedLineThickness = 20.0;

	[ObservableProperty]
	private Color selectedColor = Colors.Blue;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double SelectedLineThickness
	{
		get
		{
			return selectedLineThickness;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(selectedLineThickness, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedLineThickness);
				selectedLineThickness = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedLineThickness);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Color SelectedColor
	{
		get
		{
			return selectedColor;
		}
		set
		{
			if (!EqualityComparer<Color>.Default.Equals(selectedColor, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedColor);
				selectedColor = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedColor);
			}
		}
	}
}

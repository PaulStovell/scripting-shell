﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Sheldon.Extensions
{
    /// <summary>
    /// Provides attached dependency properties to enable auto scrolling when the text of a TextBox changes. 
    /// </summary>
    public class AutoScrollingTextBox
    {
        public static readonly DependencyProperty ScrollToBottomOnTextChangedProperty = DependencyProperty.RegisterAttached("ScrollToBottomOnTextChanged", typeof(object), typeof(AutoScrollingTextBox), new UIPropertyMetadata(null, ScrollToBottomOnTextChangedPropertySet));

        public static object GetScrollToBottomOnTextChanged(TextBoxBase target)
        {
            return target.GetValue(ScrollToBottomOnTextChangedProperty);
        }

        public static void SetScrollToBottomOnTextChanged(TextBoxBase target, object value)
        {
            target.SetValue(ScrollToBottomOnTextChangedProperty, value);
        }

        private static void ScrollToBottomOnTextChangedPropertySet(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var targetTextBox = (TextBoxBase)target;
            targetTextBox.TextChanged -= TargetTextBox_TextChanged;
            if (e.NewValue != null)
            {
                targetTextBox.TextChanged += TargetTextBox_TextChanged;
            }
        }

        private static void TargetTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var targetTextBox = (TextBoxBase)sender;
            var scrollViewer = GetScrollToBottomOnTextChanged(targetTextBox) as ScrollViewer;
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToBottom();
            }
        }
    }
}
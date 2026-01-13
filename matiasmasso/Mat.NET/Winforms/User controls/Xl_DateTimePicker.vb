Imports System.ComponentModel

Public Class Xl_DateTimePicker
    Inherits DateTimePicker

    Public Shadows Event ValueChanged(sender As Object, e As EventArgs)

    Private Sub DateTimePicker1_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.DropDown
        'to avoid valuechanged when browsing months
        RemoveHandler MyBase.ValueChanged, AddressOf Xl_DateTimePicker_ValueChanged
    End Sub

    Private Sub DateTimePicker1_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.CloseUp
        'to avoid valuechanged when browsing months
        AddHandler MyBase.ValueChanged, AddressOf Xl_DateTimePicker_ValueChanged
        Call Xl_DateTimePicker_ValueChanged(sender, EventArgs.Empty)
    End Sub

    Private Sub Xl_DateTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles MyBase.ValueChanged
        RaiseEvent ValueChanged(sender, e)
    End Sub
End Class

Public Class Xl_NumSS
    Inherits TextBox
    Private _Tooltip As ToolTip

    Private _value As String
    Private _exs As List(Of Exception)

    Public Shadows Sub Load(value As String)
        _Tooltip = New ToolTip
        MyBase.Text = value
    End Sub

    ReadOnly Property Value As String
        Get
            Return _value
        End Get
    End Property

    Private Sub Validate()
        Dim oNumSs As New DTONumSs(MyBase.Text)
        _exs = New List(Of Exception)
        If oNumSs.IsValid(_exs) Then
            MyBase.Text = oNumSs.formatted
            MyBase.BackColor = LegacyHelper.Defaults.COLOR_OK
            _Tooltip.SetToolTip(Me, "")
        Else
            MyBase.BackColor = LegacyHelper.Defaults.COLOR_NOTOK
            _Tooltip.SetToolTip(Me, ExceptionsHelper.ToFlatString(_exs))
        End If
    End Sub

    Private Sub Xl_NumSS_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        Validate()
    End Sub

End Class

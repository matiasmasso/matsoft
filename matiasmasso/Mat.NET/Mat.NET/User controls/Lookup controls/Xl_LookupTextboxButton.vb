Imports System.ComponentModel

Public Class Xl_LookupTextboxButton
    Private _Value As Object
    Private _IsDirty As Boolean

    Public Shadows Event TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Shadows Event Doubleclick(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Property Value() As Object
        Get
            Return _Value
        End Get
        Set(ByVal value As Object)
            _Value = value
        End Set
    End Property

    Public Overrides Property Text() As String
        Get
            Return TextBox1.Text
        End Get
        Set(ByVal value As String)
            TextBox1.Text = value
        End Set
    End Property

    Public Property IsDirty() As Boolean
        Get
            Return _IsDirty
        End Get
        Set(ByVal value As Boolean)
            _IsDirty = value
        End Set
    End Property

    <Description("Test text displayed in the textbox"), Category("Data"), Browsable(True), EditorBrowsable(EditorBrowsableState.Always)> _
    Public Property PasswordChar() As String
        Get
            Return TextBox1.PasswordChar
        End Get
        Set(ByVal value As String)
            TextBox1.PasswordChar = value
        End Set
    End Property

    '<Description("Test text displayed in the textbox"), Category("Data"), Browsable(True), EditorBrowsable(EditorBrowsableState.Always)> _
    'Public Property [ReadOnly] As Boolean
    '    Get
    '        Return TextBox1.ReadOnly
    '    End Get
    '    Set(value As Boolean)
    '        TextBox1.ReadOnly = value
    '        TextBox1.Enabled = Not value
    '    End Set
    'End Property

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        RaiseEvent onLookUpRequest(sender, e)
    End Sub

    Private Sub TextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DoubleClick
        RaiseEvent Doubleclick(sender, e)
    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        Dim position As Integer = TextBox1.Text.Length
        TextBox1.Select(position, position)
    End Sub


    Protected Sub SetContextMenuRange(oMenuRange As ToolStripMenuItem())
        Dim oContextMenu As New ContextMenuStrip
        oContextMenu.Items.AddRange(oMenuRange)
        TextBox1.ContextMenuStrip = oContextMenu
    End Sub

    Protected Sub ClearContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        TextBox1.ContextMenuStrip = oContextMenu
    End Sub
End Class

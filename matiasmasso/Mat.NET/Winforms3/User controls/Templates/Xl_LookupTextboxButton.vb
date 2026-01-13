Imports System.ComponentModel

Public Class Xl_LookupTextboxButton
    Private _Value As Object
    Private _BackGroundColorWhenNotNull As Color
    Private _DefalutBackGroundColor As Color
    Private _IsDirty As Boolean

    Public Shadows Event TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event onLookUpRequest(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Shadows Event Doubleclick(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New()
        MyBase.New
        InitializeComponent()
        _DefalutBackGroundColor = MyBase.BackColor
        TextBox1.ReadOnly = False
        Button1.Enabled = True
        Me.ReadOnlyLookup = False
    End Sub

    Public Property Value() As Object
        Get
            Return _Value
        End Get
        Set(ByVal value As Object)
            _Value = value
        End Set
    End Property

    Public Property ReadOnlyLookup() As Boolean
        Get
            Return TextBox1.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            TextBox1.ReadOnly = value
            Button1.Enabled = Not value
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

    Public WriteOnly Property BackGroundColorWhenNotNull As Color
        Set(value As Color)
            _BackGroundColorWhenNotNull = value
        End Set
    End Property

    Public Shadows WriteOnly Property BackColor As Color
        Set(value As Color)
            TextBox1.BackColor = value
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
        RaiseEvent onLookUpRequest(sender, New MatEventArgs(_Value))
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

    Protected Function Lang() As DTOLang
        Dim retval As DTOLang = Current.Session.Lang
        Return retval
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If _BackGroundColorWhenNotNull <> Nothing Then
            If TextBox1.Text.Length = 0 Then
                TextBox1.BackColor = _BackGroundColorWhenNotNull
            Else
                TextBox1.BackColor = _BackGroundColorWhenNotNull
            End If
        End If
    End Sub
End Class

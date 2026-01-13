Public Class Xl_Address
    Inherits RichTextBox

    Private _Address As DTOAddress
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New()
        MyBase.New
        MyBase.Multiline = True
        MyBase.Size = New Size(431, 31)
        MyBase.ReadOnly = True
    End Sub

    Public Sub Load(oAddress As DTOAddress)
        _Address = oAddress
        Dim s As String = DTOAddress.MultiLine(_Address)
        If s = "" Then
            MyBase.Text = "(doble click per crear adreça)"
        Else
            MyBase.Text = DTOAddress.MultiLine(_Address)
        End If
        SetContextMenu()
    End Sub

    Public ReadOnly Property Address As DTOAddress
        Get
            Return _Address
        End Get
    End Property

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenu_Adr As New Menu_Adr(_Address)
        AddHandler oMenu_Adr.AfterUpdate, AddressOf refreshRequest
        oContextMenu.Items.AddRange(oMenu_Adr.Range)
        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub refreshRequest(sender As Object, e As MatEventArgs)
        _Address = e.Argument
        MyBase.Text = DTOAddress.MultiLine(_Address)
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Address))
    End Sub

    Private Sub Xl_Adr4_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Dim oFrm As New Frm_Address(_Address)
        AddHandler oFrm.AfterUpdate, AddressOf refreshRequest
        oFrm.Show()
    End Sub



    Private Sub Xl_Adr4_SelectionChanged(sender As Object, e As EventArgs) Handles Me.SelectionChanged
        MyBase.SelectionLength = 0
    End Sub


End Class

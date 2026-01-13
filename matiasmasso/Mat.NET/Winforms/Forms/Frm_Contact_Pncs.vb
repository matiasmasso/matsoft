Public Class Frm_Contact_Pncs
    Private _Contact As DTOContact
    Private _Codi As DTOPurchaseOrder.Codis
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oContact As DTOContact, oCod As DTOPurchaseOrder.Codis, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
        _Contact = oContact
        _Codi = oCod
    End Sub

    Private Async Sub Frm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        FEB2.Contact.Load(_Contact, exs)
        Me.Text = "Comandes pendents de " & _Contact.FullNom
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB2.PurchaseOrderItems.Pending(exs, _Contact, _Codi, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then
            Xl_Contact_Pncs1.Load(items, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Contact_Pncs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contact_Pncs1.RequestToRefresh
        Await refresca()
    End Sub


    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim sSearchKey As String = TextBoxSearch.Text
        If sSearchKey.Length > 1 Then
            TextBoxSearch.ForeColor = Color.Black
            Xl_Contact_Pncs1.Filter = sSearchKey
        Else
            Xl_Contact_Pncs1.ClearFilter()
            TextBoxSearch.ForeColor = Color.Gray
        End If

    End Sub


    Private Sub Xl_Contact_Pncs1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Contact_Pncs1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub
End Class
Public Class Frm_Contact_Pncs
    Private _Contact As DTOContact
    Private _Codi As DTOPurchaseOrder.Codis

    Public Sub New(oContact As DTOContact, oCod As DTOPurchaseOrder.Codis)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
        _Codi = oCod
    End Sub

    Private Sub Frm_Load(sender As Object, e As EventArgs) Handles Me.Load
        BLL.BLLContact.Load(_Contact)
        Me.Text = "Comandes pendents de " & _Contact.FullNom
        refresca()
    End Sub

    Private Sub refresca()
        Dim items As List(Of DTOPurchaseOrderItem) = BLL.BLLPurchaseOrderItems.Pending(_Contact, _Codi)
        Xl_Contact_Pncs1.Load(items)
    End Sub

    Private Sub Xl_Contact_Pncs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contact_Pncs1.RequestToRefresh
        refresca()
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

End Class
Public Class Frm_RepPdcFollowUp
    Private _Order As DTOPurchaseOrder
    Private _Pnc As DTOPurchaseOrderItem

    Public Sub New(oOrder As DTOPurchaseOrder)
        MyBase.New
        InitializeComponent()
        _Order = oOrder
    End Sub

    Public Sub New(oPnc As DTOPurchaseOrderItem)
        MyBase.New
        InitializeComponent()
        _Pnc = oPnc
    End Sub

    Private Async Sub Frm_RepPdcFollowUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Pnc Is Nothing Then
            Dim oRep As DTORep = Await FEB.Rep.Find(_Pnc.RepCom.Rep.Guid, exs)
            If exs.Count = 0 Then
                Me.Text = Me.Text & " " & oRep.NickName & " " & _Pnc.PurchaseOrder.FullNomAndCaption
                TextBox1.Text = _Pnc.Sku.RefYNomLlarg.Tradueix(Current.Session.Lang)
                Xl_RepPdcFollowUp1.Load(_Pnc)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            Dim oRep As DTORep = _Order.Items.Select(Function(x) x.RepCom.Rep).First
            oRep = Await FEB.Rep.Find(oRep.Guid, exs)
            If exs.Count = 0 Then
                Me.Text = Me.Text & " " & oRep.NickName
                TextBox1.Text = _Order.FullNomAndCaption(oRep.Lang)
                Xl_RepPdcFollowUp1.Load(_Order.Items)
            Else
                UIHelper.WarnError(exs)
            End If

        End If
    End Sub
End Class
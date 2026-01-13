Public Class Frm_SkuPrevisions
    Private _Sku As DTOProductSku

    Public Sub New(oSku As DTOProductSku)
        MyBase.New
        InitializeComponent()
        _Sku = oSku

    End Sub

    Private Async Sub Frm_SkuPrevisions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.ProductSku.Load(_Sku, exs) Then
            Me.Text = String.Format("Previsions entrada {0}", _Sku.nomLlarg.Tradueix(Current.Session.Lang))
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim values = Await FEB2.ImportPrevisions.All(exs, _Sku)
        If exs.Count = 0 Then
            Xl_SkuPrevisions1.Load(values)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_SkuPrevisions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SkuPrevisions1.RequestToRefresh
        Await refresca()
    End Sub
End Class
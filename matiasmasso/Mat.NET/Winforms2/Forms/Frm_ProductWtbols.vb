Public Class Frm_ProductWtbols
    Private _Product As DTOProduct

    Public Sub New(oProduct As DTOProduct)
        MyBase.New
        Me.InitializeComponent()
        _Product = oProduct
    End Sub

    Private Async Sub Frm_ProductWtbols_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Product.Load(_Product, exs) Then
            Me.Text = Me.Text = _Product.FullNom()
            Dim items = Await FEB.WtbolLandingPages.All(exs, _Product)
            If exs.Count = 0 Then
                Xl_ProductWtbols1.Load(items)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

End Class
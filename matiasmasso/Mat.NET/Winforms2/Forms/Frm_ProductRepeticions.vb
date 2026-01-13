Public Class Frm_ProductRepeticions
    Private _Product As DTOProduct
    Private _Values As List(Of DTORepeticio)
    Private _AllowEvents As Boolean

    Public Sub New(oProduct As DTOProduct)
        MyBase.New
        InitializeComponent()
        _Product = oProduct
        _AllowEvents = True
    End Sub

    Private Async Sub Frm_ProductRepeticions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Product.Load(_Product, exs) Then
            Me.Text = "Repeticions de " & _Product.nom.Tradueix(Current.Session.Lang)
            If Await LoadParent(exs) Then
                LoadChildren()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ProductRepeticionsParent1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductRepeticionsParent1.ValueChanged
        If _AllowEvents Then LoadChildren()
    End Sub

    Private Async Function LoadParent(exs As List(Of Exception)) As Task(Of Boolean)
        _Values = Await FEB.Repeticions.All(exs, _Product)
        If exs.Count = 0 Then
            Xl_ProductRepeticionsParent1.Load(_Values)
        End If
        Return exs.Count = 0
    End Function

    Private Sub LoadChildren()
        Dim iOrdersCount As Integer = Xl_ProductRepeticionsParent1.Value
        Dim items As List(Of DTORepeticio) = _Values.Where(Function(x) x.Orders = iOrdersCount).ToList
        Xl_ProductRepeticionsChildren1.Load(items)
    End Sub
End Class
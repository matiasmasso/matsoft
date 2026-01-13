Public Class Frm_PromofarmaOrder
    Private _Cache As Models.ClientCache
    Private _Order As DTO.Integracions.Promofarma.Order
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTO.Integracions.Promofarma.Order)
        MyBase.New()
        Me.InitializeComponent()
        _Order = value
        Me.Text = String.Format("Comanda {0} per {1}", _Order.data.order_id, _Order.data.customer_name)
    End Sub

    Private Async Sub Frm_PromofarmaOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Cache = Await FEB.Cache.Fetch(exs, Current.Session.User)
        If exs.Count = 0 Then
            Dim lines = Await FEB.PromofarmaOrder.OrderLines(exs, _Order)
            If exs.Count = 0 Then
                With _Order
                    TextBoxFch.Text = .data.order_date
                    TextBoxNom.Text = .data.customer_name
                    TextBoxPais.Text = .data.customer_country
                    Xl_PromofarmaOrderLines1.Load(_Cache, lines)
                End With
                ProgressBar1.Visible = False
                _AllowEvents = True
            Else
                ProgressBar1.Visible = False
                UIHelper.WarnError(exs)
            End If
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

End Class
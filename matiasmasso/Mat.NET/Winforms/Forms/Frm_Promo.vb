Public Class Frm_Promo

    Private _Promo As DTOPromo
    Private _Orders As List(Of DTOPurchaseOrder)
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Comandes
    End Enum

    Public Sub New(value As DTOPromo)
        MyBase.New()
        Me.InitializeComponent()
        _Promo = value
        BLL.BLLPromo.Load(_Promo)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Promo
            TextBoxCaption.Text = .Caption
            If .FchFrom >= DateTimePickerFchFrom.MinDate Then
                DateTimePickerFchFrom.Value = .FchFrom
            End If
            If .FchTo >= DateTimePickerFchTo.MinDate Then
                DateTimePickerFchTo.Value = .FchTo
            End If
            If .Product IsNot Nothing Then
                Xl_LookupProduct1.Product = .Product
            End If
            TextBoxBases.Text = .Bases
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxCaption.TextChanged,
         Xl_LookupProduct1.AfterUpdate,
          DateTimePickerFchFrom.ValueChanged,
           DateTimePickerFchTo.ValueChanged,
            TextBoxBases.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Promo
            .Caption = TextBoxCaption.Text
            .Product = Xl_LookupProduct1.Product
            .FchFrom = DateTimePickerFchFrom.Value
            .FchTo = DateTimePickerFchTo.Value
            .Bases = TextBoxBases.Text
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLPromo.Update(_Promo, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Promo))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLPromo.Delete(_Promo, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Promo))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


    Private Sub Xl_PromosPerRep1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_PromosPerRep1.ValueChanged
        If _AllowEvents Then
            Dim oRep As DTORep = e.Argument
            Dim oOrders As List(Of DTOPurchaseOrder)
            If oRep.Guid.Equals(Guid.Empty) Then
                oOrders = _Orders
            Else
                oOrders = _Orders.Where(Function(x) x.Items.Any(Function(y) MatchesRep(y.RepCom, oRep))).ToList
            End If
            Xl_PurchaseOrders1.Load(oOrders)
        End If
    End Sub

    Private Function MatchesRep(oRepCom As DTORepCom, oRep As DTORep) As Boolean
        Dim retval As Boolean
        If oRepCom IsNot Nothing Then
            If oRepCom.Rep.Equals(oRep) Then
                retval = True
            End If
        End If
        Return retval
    End Function

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case CType(TabControl1.SelectedIndex, Tabs)
            Case Tabs.General
            Case Tabs.Comandes
                Static loaded As Boolean
                If Not loaded Then
                    _AllowEvents = False
                    _Orders = BLLPromo.PurchaseOrders(_Promo, BLLSession.Current.User)
                    Xl_PromosPerRep1.Load(_Orders)
                    Xl_PurchaseOrders1.Load(_Orders)
                    _AllowEvents = True
                End If
        End Select
    End Sub
End Class



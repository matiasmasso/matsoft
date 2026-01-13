Public Class Frm_OrderConfirmation
    Private _OrderConfirmation As OrderConfirmation
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As OrderConfirmation)
        MyBase.New()
        Me.InitializeComponent()
        _OrderConfirmation = value
    End Sub

    Private Sub Frm_OrderConfirmation_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _OrderConfirmation
            Xl_ContactSupplier.Contact = .SupplierContact
            Xl_ContactBuyer.Contact = .BuyerContact
            Xl_ContactDelivery.Contact = .DeliveryContact
            Xl_ContactInvoice.Contact = .InvoiceContact
            Xl_AmtCur1.Amt = .Total
            DateTimePicker1.Value = .Fch
            TextBoxOrderConfirmationNum.Text = .DocumentNumber
            If .Pdc IsNot Nothing Then
                TextBoxPdc.Text = .Pdc.Id & " " & .Pdc.Fch.ToShortDateString
                Dim oMenu As New Menu_Pdc(.Pdc)
                Dim oContextMenu As New ContextMenuStrip
                oContextMenu.Items.AddRange(oMenu.Range)
                TextBoxPdc.ContextMenuStrip = oContextMenu
            End If
        End With
        Xl_OrderConfirmationItems1.Load(_OrderConfirmation)
        ButtonOk.Enabled = _OrderConfirmation.IsNew
    End Sub

    Private Sub Do_ZoomPdc()
        Dim oPdc As Pdc = _OrderConfirmation.Pdc
        Dim exs As New List(Of Exception)
        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, New DTOContact(oPdc.Client.Guid), DTOAlbBloqueig.Codis.PDC, exs) Then
            UIHelper.WarnError(exs)
        Else
            Dim oFrm As New Frm_Pdc_Proveidor(oPdc)
            AddHandler oFrm.AfterUpdate, AddressOf onPdcUpdate
            oFrm.Show()
        End If
    End Sub

    Private Sub onPdcUpdate()
        TextBoxPdc.Text = _OrderConfirmation.Pdc.FullConcepte
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta confirmació de comanda?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            If OrderConfirmationLoader.Delete(_OrderConfirmation, exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError( exs, "no s'ha pogut eliminar la confirmacio de comanda")
            End If
        End If
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        Xl_ContactSupplier.AfterUpdate, _
 _
          TextBoxOrderConfirmationNum.TextAlignChanged, _
           DateTimePicker1.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub


    Private Sub ButtonBrowsePdc_Click(sender As Object, e As EventArgs) Handles ButtonBrowsePdc.Click
        Do_ZoomPdc()
    End Sub
End Class
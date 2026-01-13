Public Class Frm_SepaCreditTransferFactory
    Private _BancTransfer As DTOBancTransferOld
    Private _AllowEvents As Boolean

    Public Sub New(oBancTransfer As DTOBancTransferOld)
        MyBase.New
        InitializeComponent()
        _BancTransfer = oBancTransfer
    End Sub

    Private Sub Frm_SepaCreditTransferFactory_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oBanc As New DTOBanc(_BancTransfer.BancEmisorItem.Contact.Guid)
        BLLContact.Load(oBanc)
        BLLBanc.Load(oBanc)
        With _BancTransfer
            Xl_LookupBanc1.Banc = oBanc
            DateTimePicker1.Value = .Fch
            Dim oBeneficiaris As List(Of DTOCcb) = BLLBancTransferOld.Beneficiaris(_BancTransfer)
            Xl_SepaCreditTransferItems1.Load(oBeneficiaris)
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _BancTransfer
            .Fch = DateTimePicker1.Value
            Dim oEmisor As DTOBancTransferItem = .BancEmisorItem
            .Items = Xl_SepaCreditTransferItems1.Values
            .Items.Insert(0, oEmisor)
        End With

        Dim exs As New List(Of Exception)
        If BLLBancTransferOld.Update(_BancTransfer, exs) Then
            'Dim XmlSource As String = BLLSepaCreditTransfer.XML(_BancTransfer)
            'Dim sFilename As String = BLLSepaCreditTransfer.DefaultFilename(_BancTransfer)
            'UIHelper.SaveXmlFileDialog(XmlSource, sFilename)
            'Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_SepaCreditTransferItems1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_SepaCreditTransferItems1.RequestToAddNew
        Dim value As New DTOBancTransferItem
        Dim oFrm As New Frm_BancTransferItem(value)
        AddHandler oFrm.AfterUpdate, AddressOf onAddedItem
        oFrm.Show()
    End Sub

    Private Sub onAddedItem(sender As Object, e As MatEventArgs)
        Dim items As List(Of DTOCcb) = Xl_SepaCreditTransferItems1.Values
        items.Add(e.Argument)
        Xl_SepaCreditTransferItems1.Load(items)
        ButtonOk.Enabled = True
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        DateTimePicker1.ValueChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_SepaCreditTransferItems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SepaCreditTransferItems1.RequestToRefresh
        ButtonOk.Enabled = True
    End Sub
End Class
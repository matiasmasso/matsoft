

Public Class Frm_FileImport
    Private mStream As Byte()
    Private mFilename As String

    Private Enum Modes
        FraProveidor
        Pago
        Cca
        Mail
        Nominas
        Pdc
    End Enum

    Public WriteOnly Property DocFile() As DTODocFile
        Set(ByVal value As DTODocFile)
            Xl_DocFile1.Load(value)
            LoadModes()
        End Set
    End Property

    Public WriteOnly Property Contact() As Contact
        Set(ByVal value As Contact)
            If value IsNot Nothing Then
                Xl_Contact1.Contact = value
                PictureBoxContact.Visible = False
                Xl_Contact1.Visible = False
                Me.Text = "IMPORTAR FITXER A " & value.Nom
            End If
        End Set
    End Property

    Private Sub EnableButtons()
        Dim BlEnable As Boolean = True
        Select Case ListBox1.SelectedIndex
            Case Is < 0
                BlEnable = False
            Case Modes.Nominas
                BlEnable = True
            Case Else
                If Xl_Contact1.Contact Is Nothing Then BlEnable = False
        End Select
        ButtonOk.Enabled = BlEnable
    End Sub

    Private Sub LoadModes()

        With ListBox1.Items
            .Clear()
            .Add("FACTURA PROVEIDOR")
            .Add("PAGAMENT A PROVEIDOR")
            .Add("ASSENTAMENT")
            .Add("CORRESPONDENCIA")
            .Add("NOMINAS")
            .Add("COMANDA CLIENT")
        End With
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        EnableButtons()
    End Sub

    Private Sub Xl_Contact1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Contact1.AfterUpdate
        EnableButtons()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oMode As Modes = ListBox1.SelectedIndex
        Dim DtFch As Date = DateTimePicker1.Value
        Select Case oMode
            Case Modes.FraProveidor
                Dim oPrv As New Proveidor(Xl_Contact1.Contact.Guid)
                Dim oFrm As New Wz_Proveidor_NewFra(oPrv, DtFch, Xl_DocFile1.Value)
                oFrm.Show()
            Case Modes.Pago
                Dim oPrv As New Proveidor(Xl_Contact1.Contact.Guid)
                root.WzPagament(oPrv, DtFch, Xl_DocFile1.Value)
            Case Modes.Cca
                Dim oCca As new cca(BLL.BLLApp.emp)
                With oCca
                    '.BigFile = New BigFileSrc(DTODocFile.Cods.Assentament, .Guid, oBigFile)
                    .fch = DtFch
                    .Txt = Xl_Contact1.Contact.Nom
                End With
                root.ShowCca(oCca)
            Case Modes.Mail
                Dim oMail As New Mail(BLL.BLLApp.Emp, DtFch)
                With oMail
                    '.BigFile = New BigFileSrc(DTODocFile.Cods.Correspondencia, .Guid, oBigFile)
                    .Cod = DTO.DTOCorrespondencia.Cods.Rebut
                    .Contacts.Add(Xl_Contact1.Contact)
                End With
                root.ShowContactMail(oMail)

            Case Modes.Nominas
                Dim oDocFile As DTODocFile = Xl_DocFile1.Value
                Dim sFilename As String = BLL_DocFile.FileNameOrDefault(oDocFile)
                If sFileName.Contains("\") Then
                Else
                    sFilename = MaxiSrvr.TmpFolder & BLL_DocFile.FileNameOrDefault(oDocFile)
                    Dim exs As New List(Of Exception)
                    If Not BLL.BLLDocFile.Save(oDocFile, sFilename, exs) Then
                        UIHelper.WarnError(exs, "error al desar el fitxer")
                        Exit Sub
                    End If
                End If
                Dim oFrm As New Frm_NominasNew(sFilename, DateTimePicker1.Value)
                oFrm.Show()

            Case Modes.Pdc
                Dim oPurchaseOrder As New DTOPurchaseOrder()
                With oPurchaseOrder
                    .Fch = DateTimePicker1.Value
                    .Cod = DTOPurchaseOrder.Codis.Client
                    .Customer = New DTOCustomer(Xl_Contact1.Contact.Guid)
                    .DocFile = Xl_DocFile1.Value
                End With
                Dim exs As New List(Of Exception)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                    oFrm.Show()
                End If
        End Select
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class
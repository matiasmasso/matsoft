

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

    Public WriteOnly Property Contact() As DTOContact
        Set(ByVal value As DTOContact)
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

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim oMode As Modes = ListBox1.SelectedIndex
        Dim DtFch As Date = DateTimePicker1.Value
        Select Case oMode
            Case Modes.FraProveidor
                Dim oPrv As DTOProveidor = DTOProveidor.FromContact(Xl_Contact1.Contact)
                Dim oFrm As New Wz_Proveidor_NewFra(oPrv, DtFch, Xl_DocFile1.Value)
                oFrm.Show()
            Case Modes.Pago
                Dim oProveidor As DTOProveidor = DTOProveidor.FromContact(Xl_Contact1.Contact)
                Dim oFrm As New Frm_Pagament(oProveidor, DtFch, Xl_DocFile1.Value)
                oFrm.Show()
            Case Modes.Cca
                Dim oCca As DTOCca = DTOCca.Factory(DtFch, Current.Session.User, DTOCca.CcdEnum.Manual)
                oCca.Concept = Xl_Contact1.Contact.Nom
                Dim oFrm As New Frm_Cca(oCca)
                oFrm.Show()
            Case Modes.Mail
                Dim oMail = DTOCorrespondencia.Factory(Current.Session.User, Xl_Contact1.Contact, DTOCorrespondencia.Cods.Rebut)
                With oMail
                    .Fch = DtFch
                End With
                Dim oFrm As New Frm_Correspondencia(oMail)
                oFrm.Show()

            Case Modes.Nominas
                Dim oDocFile As DTODocFile = Xl_DocFile1.Value
                Dim sFilename As String = FEB.DocFile.FileNameOrDefault(oDocFile)
                If sFilename.Contains("\") Then
                Else
                    sFilename = FileSystemHelper.TmpFolder & "\" & FEB.DocFile.FileNameOrDefault(oDocFile)
                    If Not UIHelper.Save(oDocFile, sFilename, exs) Then
                        UIHelper.WarnError(exs, "error al desar el fitxer")
                        Exit Sub
                    End If
                End If
                Dim oFrm As New Frm_NominasFactory(sFilename)
                oFrm.Show()

            Case Modes.Pdc
                Dim oCustomer = DTOCustomer.FromContact(Xl_Contact1.Contact)
                If FEB.Customer.Load(oCustomer, exs) Then
                    Dim oPurchaseOrder = DTOPurchaseOrder.Factory(oCustomer, Current.Session.User, DateTimePicker1.Value)
                    With oPurchaseOrder
                        .DocFile = Xl_DocFile1.Value
                    End With
                    If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oPurchaseOrder.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
        End Select
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class
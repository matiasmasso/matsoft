

Public Class Xl_Contact2_Client
    Implements IUpdatableDetailsPanel

    Private mContact As Contact = Nothing
    Private mClient As Client = Nothing
    Private mDirtyGral As Boolean = False
    Private mDirtyClx As Boolean = False
    Private mDirtyAdrEntregas As Boolean = False 'actua nomes sobre el checkbox
    Private mAllowEvents As Boolean = False
    Private mDsCliTpas As DataSet
    Private mDto As Decimal = 0

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Implements IUpdatableDetailsPanel.AfterUpdate

    Private Enum ColCliTpas
        Cod
        Ico
        Tpa
        Dsc
    End Enum

    Public Sub New(ByVal oContact As Contact)
        MyBase.New()
        Me.InitializeComponent()
        mContact = oContact
        mClient = new Client(oContact.Guid)
        With mClient
            If Not .DeliveryAdr.Equals(.Adr) Then
                CheckBoxAdrEntregas.Checked = True
                Xl_AdrEntregas.Adr = .DeliveryAdr
            End If
            SetAdrPostalVisibility()

            If .DeliveryPlatform IsNot Nothing Then
                CheckBoxPlataforma.Checked = True
                Xl_ContactPlataforma.Contact = .DeliveryPlatform
                Xl_ContactPlataforma.Visible = True
            End If

            If .Ccx IsNot Nothing Then
                CheckBoxFacturarA.Checked = .Ccx.Exists
            End If
            If CheckBoxFacturarA.Checked Then
                Xl_ContactCcx.Contact = .Ccx
                TextBoxRef.Text = .Referencia
                CheckboxOrdersToCentral.Checked = .OrdersToCentral
            End If
            SetCcxVisibility()

            CheckBoxWarnAlbs.Checked = (.WarnAlbs > "")
            TextBoxWarnAlbs.Text = .WarnAlbs
            TextBoxObsComercials.Text = .ObsComercials

            ComboBoxPorts.SelectedIndex = .PortsCod
            SetPortsCondicions(.PortsCondicions)
            ComboBoxTarifa.SelectedIndex = CInt(.Tarifa)

            CheckBoxValorarAlbarans.Checked = .ValorarAlbarans
            CheckBoxDtoFixe.Checked = .DtoExist
            CheckBoxIVA.Checked = .IVA
            CheckBoxReq.Checked = .REQ

            ComboBoxAlbsXFra.SelectedIndex = CInt(.CodAlbsXFra)
            CheckBoxFrasIndepents.Checked = .FrasIndependents
            NumericUpDownCopiasFra.Value = .CopiasFra
            ComboBoxCash.SelectedIndex = .CashCod
            Xl_FormaDePago1.LoadFromContact(Contact.Tipus.Client, oContact, .FormaDePago)

            CheckBoxGrandesCuentas.Checked = .GrandesCuentas
            CheckBoxNoRep.Checked = .NoRep
            CheckBoxNoWeb.Checked = .NoWeb
            CheckBoxNoIncentius.Checked = .NoIncentius
            CheckBoxNoSsc.Checked = .NoSsc
            TextBoxProvNum.Text = .SuProveedorNum

            LabelRisc.Text = .RiscText
            CheckBoxNoASNEF.Checked = .NoAsnef

            ButtonTarifaCustom.Enabled = .HasTarifaCustom
        End With

        mAllowEvents = True
    End Sub

    Public ReadOnly Property Dirty() As Boolean
        Get
            Dim Retval As Boolean = False
            If mDirtyGral Then Retval = True
            If mDirtyClx Then Retval = True
            If mDirtyAdrEntregas Then Retval = True
            If Xl_AdrEntregas.IsDirty Then Retval = True
            Return Retval
        End Get
    End Property

    Private Sub SetDirtyGral()
        If mAllowEvents Then
            mDirtyGral = True
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
        End If
    End Sub

    Private Sub SetDirtyClx()
        If mAllowEvents Then
            mDirtyClx = True
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
        End If
    End Sub

    Private Sub SetDirtyAdrEntregas()
        If mAllowEvents Then
            mDirtyAdrEntregas = True
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
        End If
    End Sub

    Private Sub GRAL_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         Xl_FormaDePago1.AfterUpdate, _
         CheckBoxFacturarA.CheckedChanged, _
         CheckboxOrdersToCentral.CheckedChanged, _
         CheckBoxFrasIndepents.CheckedChanged, _
         CheckBoxIVA.CheckedChanged, _
         CheckBoxReq.CheckedChanged, _
         CheckBoxNoSsc.CheckedChanged, _
         CheckBoxNoRep.CheckedChanged, _
         CheckBoxNoWeb.CheckedChanged, _
         CheckBoxNoIncentius.CheckedChanged, _
         CheckBoxGrandesCuentas.CheckedChanged, _
         CheckBoxValorarAlbarans.CheckedChanged, _
         ComboBoxPortsCondicions.SelectedIndexChanged, _
         ComboBoxPorts.SelectedIndexChanged, _
         ComboBoxTarifa.SelectedIndexChanged, _
         ComboBoxCash.SelectedIndexChanged, _
         ComboBoxAlbsXFra.SelectedIndexChanged, _
 _
         TextBoxObsComercials.TextChanged, _
         TextBoxWarnAlbs.TextChanged, _
         CheckBoxNoASNEF.CheckedChanged, _
          Xl_ContactPlataforma.AfterUpdate

        If mAllowEvents Then
            SetDirtyGral()
        End If
    End Sub

    Private Sub GRAL_CLX_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         TextBoxRef.TextChanged

        SetDirtyGral()
        SetDirtyClx()
    End Sub


    Private Sub Xl_AdrEntregas_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_AdrEntregas.AfterUpdate, _
    CheckBoxAdrEntregas.CheckedChanged
        SetDirtyAdrEntregas()
        SetAdrPostalVisibility()
    End Sub

    Private Sub CheckBoxWarnAlbs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxWarnAlbs.CheckedChanged
        TextBoxWarnAlbs.Visible = CheckBoxWarnAlbs.Checked
        If mAllowEvents Then
            SetDirtyGral()
        End If
    End Sub

    Private Sub CheckBoxFacturarA_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    CheckBoxFacturarA.CheckedChanged
        If mAllowEvents Then
            SetCcxVisibility()
            SetDirtyGral()
        End If
    End Sub

    Private Sub SetAdrPostalVisibility()
        Xl_AdrEntregas.Visible = CheckBoxAdrEntregas.Checked
    End Sub

    Private Sub SetCcxVisibility()
        Dim FacturarA As Boolean = CheckBoxFacturarA.Checked

        'enable FacturarA GroupBox
        Xl_ContactCcx.Enabled = FacturarA
        TextBoxRef.Enabled = FacturarA
        CheckboxOrdersToCentral.Enabled = FacturarA

        'oculta Ccx si facturem a un altre lloc
        GroupBoxCcx.Visible = Not FacturarA
    End Sub


    Public Function UpdateIfDirty(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.UpdateIfDirty
        Dim retval As Boolean = False
        With mClient
            If mDirtyAdrEntregas Then
                If CheckBoxAdrEntregas.Checked Then
                    .Adr = Xl_AdrEntregas.Adr
                    .Adr.Update(mClient, Adr.Codis.Entregas)
                Else
                    Adr.Delete(mClient, Adr.Codis.Entregas)
                End If
                retval = True
            End If

            If mDirtyGral Then
                If CheckBoxFacturarA.Checked And Xl_ContactCcx.Contact Is Nothing Then
                    .Ccx = New Client(Xl_ContactCcx.Contact.Guid)
                    .Referencia = TextBoxRef.Text
                    .OrdersToCentral = CheckboxOrdersToCentral.Checked
                Else
                    .Ccx = New Client(mContact.Emp)
                    .Referencia = ""
                    .OrdersToCentral = False
                End If


                If CheckBoxWarnAlbs.Checked Then
                    .WarnAlbs = TextBoxWarnAlbs.Text
                Else
                    .WarnAlbs = ""
                End If

                .ObsComercials = TextBoxObsComercials.Text
                .PortsCod = ComboBoxPorts.SelectedIndex
                .PortsCondicions = New PortsCondicions(mClient.Emp, ComboBoxPortsCondicions.SelectedValue)
                .Tarifa = ComboBoxTarifa.SelectedIndex

                .ValorarAlbarans = CheckBoxValorarAlbarans.Checked
                'dte.fixe
                .IVA = CheckBoxIVA.Checked
                .REQ = CheckBoxReq.Checked

                .CodAlbsXFra = ComboBoxAlbsXFra.SelectedIndex
                .FrasIndependents = CheckBoxFrasIndepents.Checked
                .CopiasFra = NumericUpDownCopiasFra.Value
                .CashCod = ComboBoxCash.SelectedIndex
                .FormaDePago = Xl_FormaDePago1.FormaDePago


                .GrandesCuentas = CheckBoxGrandesCuentas.Checked
                .NoRep = CheckBoxNoRep.Checked
                .NoWeb = CheckBoxNoWeb.Checked
                .NoIncentius = CheckBoxNoIncentius.Checked
                .NoSsc = CheckBoxNoSsc.Checked
                .SuProveedorNum = TextBoxProvNum.Text

                .NoAsnef = CheckBoxNoASNEF.Checked

                If CheckBoxPlataforma.Checked Then
                    .DeliveryPlatform = Xl_ContactPlataforma.Contact
                Else
                    .DeliveryPlatform = Nothing
                End If

                exs = New List(Of exception)
                If Not .Update( exs) Then
                    MsgBox("Error al desar la fitxa de client" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If
                retval = True

            End If

            If mDirtyClx Then
                .Referencia = TextBoxRef.Text
                .UpdateClx( exs)
                retval = True
            End If

        End With


        Return retval
    End Function

    Public Function AllowDelete(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.AllowDelete
        Dim retval As Boolean = False
        Return retval
    End Function

    Public Function Delete(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.Delete
        Dim retval As Boolean = False
        Return retval
    End Function


    Private Sub SetPortsCondicions(ByVal oCondicions As PortsCondicions)
        If ComboBoxPortsCondicions.Items.Count = 0 Then
            Dim SQL As String = "SELECT ID,NOM FROM PORTSCONDICIONS ORDER BY ID"
            Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
            With ComboBoxPortsCondicions
                .DataSource = oDs.Tables(0)
                .DisplayMember = "NOM"
                .ValueMember = "ID"
            End With
        End If
        ComboBoxPortsCondicions.SelectedValue = oCondicions.Id
    End Sub

    Private Sub LinkLabelCYC_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim URL As String = "http://www.creditoycaucion.es"
        Process.Start("IExplore.exe", URL)
    End Sub

    Private Sub ButtonCYC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCYC.Click
        Dim oFrm As New Frm_Client_Risc(mClient)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshCredit
        oFrm.Show()
    End Sub

    Private Sub RefreshCredit(ByVal sender As Object, ByVal e As System.EventArgs)
        With mClient
            LabelRisc.Text = .RiscText
        End With
    End Sub

    Private Sub ButtonPortsCondicions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPortsCondicions.Click
        Dim oConds As New PortsCondicions(mClient.Emp, ComboBoxPortsCondicions.SelectedValue)
        root.ShowPortsCondicions(oConds)
    End Sub

    Private Sub ButtonDtoFixe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDtoFixe.Click
        Dim oFrm As New Frm_CliDtos(mClient)
        AddHandler oFrm.AfterUpdate, AddressOf AfterDtosUpdate
        oFrm.Show()
    End Sub

    Private Sub AfterDtosUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oClient As Client = sender
        If mDto <> oClient.Dto Then
            mDto = oClient.Dto
            SetDirtyGral()
        End If
    End Sub

    Private Sub CheckBoxDtoFixe_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDtoFixe.CheckedChanged
        ButtonDtoFixe.Visible = CheckBoxDtoFixe.Checked
    End Sub

    Private Sub PictureBoxTarifasExcelLinks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxTarifasExcelLinks.Click
        Dim sToday As String = Format(Today, "yyyyMMdd")
        Dim SQL As String = "SELECT TARIFA.GUID, TARIFA.TPA, TARIFA.TARIFA, TPA.DSC FROM " _
        & "TARIFA INNER JOIN TPA ON TARIFA.EMP=TPA.EMP AND TARIFA.TPA=TPA.TPA " _
        & "WHERE TARIFA.EMP=" & mClient.Emp.Id & " AND " _
        & "MEMBRETE=1 "

        If Not mClient.Tarifa = MaxiSrvr.Client.Tarifas.Virtual Then
            SQL = SQL & "AND (TARIFA.TARIFA=" & mClient.Tarifa & " " _
            & "OR TARIFA.TARIFA=" & MaxiSrvr.Client.Tarifas.Pvp & ") "
        Else
            SQL = SQL & "AND (TARIFA.TARIFA=" & MaxiSrvr.Client.Tarifas.Standard & " " _
            & "OR TARIFA.TARIFA=" & MaxiSrvr.Client.Tarifas.Pvp & ") "
        End If

        SQL = SQL & "ORDER BY TPA.ORD, TARIFA.TARIFA"



        Dim oArrayText As New ArrayList
        Dim oArrayGuid As New ArrayList
        Dim sText As String = ""
        Dim sGuid As String = ""

        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Do While oDrd.Read
            sGuid = oDrd("Guid").ToString
            oArrayGuid.Add(sGuid)
            Select Case CType(oDrd("TARIFA"), Client.Tarifas)
                Case MaxiSrvr.Client.Tarifas.Pvp
                    sText = oDrd("DSC") & ": " & mClient.Lang.Tradueix("TARIFA PVP", "TARIFA PVP", "RETAIL PRICE LIST")
                Case Else
                    sText = oDrd("DSC") & ": " & mClient.Lang.Tradueix("TARIFA COSTE", "TARIFA COST", "DISTRIBUTOR PRICE LIST")
            End Select
            oArrayText.Add(sText)
        Loop
        MatExcel.CopyLinksToClipboard(oArrayText, oArrayGuid).Visible = True


    End Sub

    Private Sub PictureBoxTarifasExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxTarifasExcel.Click
        UIHelper.ShowHtml(mClient.UrlPro & "/tarifas")
    End Sub

    Private Sub CheckBoxFpgIndepent_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Delega As Boolean = CheckBoxFacturarA.Checked
        Xl_FormaDePago1.Visible = (Not Delega)
        SetDirtyGral()
    End Sub


    Private Sub ButtonTarifaCustom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonTarifaCustom.Click
        Dim oFrm As New Frm_CliPreusXClient(mClient.CcxOrMe)
        oFrm.Show()
    End Sub

    Private Sub CheckBoxPlataforma_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxPlataforma.CheckedChanged
        If mAllowEvents Then
            Xl_ContactPlataforma.Visible = CheckBoxPlataforma.Checked
            SetDirtyGral()
        End If
    End Sub
End Class

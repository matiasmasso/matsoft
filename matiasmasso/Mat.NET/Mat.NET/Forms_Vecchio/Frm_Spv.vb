

Public Class Frm_Spv

    Private mSpv As spv
    Private mSpvArt As SpvArt
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDsSpvinSpvs As DataSet
    Private mIncidencia As DTOIncidencia
    Private mProduct As Product = Nothing
    'Private mTargetProductCod As ProductCods = ProductCods.NotSet
    Private mAllowevents As Boolean
    Private mDirtyOutSpvIn As Boolean
    Private mDirtyOutSpvOut As Boolean
    Private mDirtyAlb As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Yea
        Id
        ArtNom
        Clx
    End Enum

    Private Enum Tabs
        General
        Entrada
        Sortida
    End Enum

    Public Sub New(ByVal oSpv As spv)
        MyBase.New()
        Me.InitializeComponent()
        mSpv = oSpv
        If mSpv IsNot Nothing Then
            mSpvArt = mSpv.Art
            If mSpv.Id = 0 Then
                Me.Text = "NOU FULL DE REPARACIÓ"
            Else
                Me.Text = "FULL DE REPARACIÓ NUM." & mSpv.Id.ToString
            End If
            Refresca()
            ButtonDel.Enabled = mSpv.AllowDelete
        End If
    End Sub

    Public Sub New(ByVal oIncidencia As DTOIncidencia)
        MyBase.New()
        Me.InitializeComponent()
        mIncidencia = oIncidencia
        mSpv = BLL_Incidencia.NewSpv(oIncidencia)
        mSpvArt = mSpv.Art
        If mSpvArt Is Nothing Then
            Me.Text = "NOU FULL DE REPARACIÓ PER LA INCIDENCIA " & mIncidencia.Id
        Else
            Me.Text = "FULL DE REPARACIÓ NUM." & mSpv.Id.ToString & " (INCIDENCIA " & mIncidencia.Id & ")"
        End If
        Refresca()
        ButtonDel.Enabled = mSpv.AllowDelete
    End Sub

    Private Sub Refresca()
        With mSpv
            Xl_Contact1.Contact = .Client
            If .Nom > "" Or .Adr IsNot Nothing Then
                CheckBoxAdr.Checked = True
                TextBoxNom.Text = .Nom
                Xl_Adr1.Adr = .Adr
            End If
            TextBoxReg.Text = .TextRegistre(BLL.BLLApp.Lang)
            TextBoxContacto.Text = .Contacto
            TextBoxsRef.Text = .sRef
            Xl_Product1.Product = .Product
            CheckBoxSolicitaGarantia.Checked = .SolicitaGarantia
            TextBoxCliObs.Text = .CliObs
            Xl_AmtSpvJob.Amt = .ValJob
            Xl_AmtSpvMaterial.Amt = .ValMaterial
            Xl_AmtSpvEmbalatje.Amt = .ValEmbalatje
            Xl_AmtSpvTransport.Amt = .ValPorts

            If .FchRead <= Date.MinValue Then
                CheckBoxRead.Checked = False
                TextBoxFchRead.Text = ""
            Else
                CheckBoxRead.Checked = True
                TextBoxFchRead.Text = .FchRead.ToShortDateString & " " & .FchRead.ToShortTimeString
            End If


            If .SpvIn IsNot Nothing Then
                Select Case .SpvIn.Id
                    Case Is < 0
                        CheckBoxOutSpvIn.Checked = True
                        TextBoxObsOutOfSpvIn.Visible = True
                        TextBoxObsOutOfSpvIn.Text = .ObsOutOfSpvIn
                        TextBoxOutSpvIn.Text = .textOutSpvIn
                    Case 0
                    Case Is > 0
                        With .SpvIn
                            CheckBoxOutSpvIn.Enabled = False
                            TextBoxExp.Text = .Exp 'setsItm
                            DateTimePickerSpvIn.Value = .Fch
                            TextBoxBultos.Text = .Bts
                            TextBoxKg.Text = .Kgs
                            TextBoxObs.Text = .Obs
                            SetGrid(.Yea, .Id)
                            TextBoxOutSpvIn.Visible = False
                        End With
                End Select
            End If

            If .UsrOutOfSpvOut IsNot Nothing Then
                CheckBoxOutSpvOut.Checked = True
                TextBoxObsOutOfSpvOut.Visible = True
                TextBoxObsOutOfSpvOut.Text = .ObsOutOfSpvOut
                TextBoxOutSpvOut.Text = .textOutSpvOut
            End If

            If .Alb IsNot Nothing Then
                Select Case .Alb.Id
                    Case Is < 0
                    Case 0
                    Case Is > 0
                        CheckBoxOutSpvOut.Enabled = False
                        TextBoxOutSpvOut.Visible = False
                        TextBoxOutSpvOut.Visible = False
                        TextBoxAlbNum.Text = "albará " & .Alb.Id & " del " & .Alb.Fch
                        CheckBoxGarantiaConfirmada.Checked = .Garantia
                        TextBoxObsTecnic.Text = .ObsTecnic
                        GetAmtsFromAlb(Xl_AmtJob.Amt, Xl_AmtMaterial.Amt, Xl_AmtEmbalatje.Amt, Xl_AmtTransport.Amt)
                        CheckBoxFacturable.Checked = .Alb.Facturable
                        GroupBoxPressupost.Enabled = False
                End Select
            End If

            If .Id = 0 Then
                LoadEmailsForLabel()
            Else
                ComboBoxEmailLabelTo.Visible = False
                If .LabelEmailedTo > "" Then
                    LabelEmailTo.Text = "etiqueta enviada a " & .LabelEmailedTo
                Else
                    LabelEmailTo.Text = "no consta envio de etiqueta per email"
                End If
            End If
        End With
        mAllowevents = True
    End Sub

    Private Sub LoadEmailsForLabel()
        Dim SQL As String = "SELECT EMAIL.Adr FROM  EMAIL INNER JOIN " _
        & "EMAIL_CLIS ON EMAIL.Guid = EMAIL_CLIS.EmailGuid " _
        & "WHERE EMAIL_CLIS.ContactGuid = @ContactGuid " _
        & "ORDER BY EMAIL.Privat, EMAIL.NoNews"
        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@ContactGuid", mSpv.Client.Guid.ToString)
        Do While oDrd.Read
            ComboBoxEmailLabelTo.Items.Add(oDrd("ADR").ToString)
        Loop
        ComboBoxEmailLabelTo.Items.Add("(altres)")
        oDrd.Close()
        Select Case ComboBoxEmailLabelTo.Items.Count
            Case 1
                ComboBoxEmailLabelTo.BackColor = Color.Salmon
            Case 2
                ComboBoxEmailLabelTo.SelectedIndex = 0
            Case Else
                ComboBoxEmailLabelTo.SelectedIndex = 0
                ComboBoxEmailLabelTo.BackColor = Color.Yellow
        End Select
    End Sub

    Private Sub SetAmtsToAlb()
        Dim oAlb As Alb = mSpv.Alb
        Dim oItm As LineItmArc
        Dim oDefault As New MaxiSrvr.Amt()
        Dim i As Integer
        For i = oAlb.Itms.Count - 1 To 0 Step -1
            oItm = oAlb.Itms(i)
            Select Case oItm.Art.Id
                Case MaxiSrvr.spv.MANO_DE_OBRA, MaxiSrvr.spv.MANO_DE_OBRA_SINCARGO, MaxiSrvr.spv.MATERIAL, MaxiSrvr.spv.EMBALATJE, MaxiSrvr.spv.TRANSPORT
                    oAlb.Itms.RemoveAt(i)
            End Select
        Next

        oItm = New LineItmArc(oAlb, , , mSpv)
        oItm.Qty = 1
        oItm.Preu = Xl_AmtJob.Amt
        If Xl_AmtJob.Amt IsNot Nothing Then
            oItm.Art = MaxiSrvr.Art.FromNum(mEmp, MaxiSrvr.Spv.MANO_DE_OBRA)
        Else
            oItm.Art = MaxiSrvr.Art.FromNum(mEmp, MaxiSrvr.Spv.MANO_DE_OBRA_SINCARGO)
        End If
        oAlb.Itms.Add(oItm)

        If Xl_AmtMaterial.Amt IsNot Nothing Then
            oItm = New LineItmArc(oAlb, , , mSpv)
            oItm.Qty = 1
            oItm.Preu = Xl_AmtMaterial.Amt
            oItm.Art = MaxiSrvr.Art.FromNum(mEmp, MaxiSrvr.Spv.MATERIAL)
            oAlb.Itms.Add(oItm)
        End If

        If Xl_AmtEmbalatje.Amt IsNot Nothing Then
            oItm = New LineItmArc(oAlb, , , mSpv)
            oItm.Qty = 1
            oItm.Preu = Xl_AmtEmbalatje.Amt
            oItm.Art = MaxiSrvr.Art.FromNum(mEmp, MaxiSrvr.Spv.EMBALATJE)
            oAlb.Itms.Add(oItm)
        End If

        If Xl_AmtTransport.Amt IsNot Nothing Then
            oItm = New LineItmArc(oAlb, , , mSpv)
            oItm.Qty = 1
            oItm.Preu = Xl_AmtTransport.Amt
            oItm.Art = MaxiSrvr.Art.FromNum(mEmp, MaxiSrvr.Spv.TRANSPORT)
            oAlb.Itms.Add(oItm)
        End If

    End Sub

    Private Sub GetAmtsFromAlb(ByRef oJob As maxisrvr.Amt, ByRef oMaterial As maxisrvr.Amt, ByRef oEmbalatje As maxisrvr.Amt, ByRef oTransport As maxisrvr.Amt)
        Dim oAlb As Alb = mSpv.Alb
        Dim oItm As LineItmArc
        Dim oDefault As New MaxiSrvr.Amt
        oJob = oDefault
        oMaterial = oDefault
        oEmbalatje = oDefault
        oTransport = oDefault
        For Each oItm In oAlb.Itms
            Select Case oItm.Art.Id
                Case MaxiSrvr.spv.MANO_DE_OBRA, MaxiSrvr.spv.MANO_DE_OBRA_SINCARGO
                    oJob = oItm.Amt
                Case MaxiSrvr.spv.MATERIAL
                    oMaterial = oItm.Amt
                Case MaxiSrvr.spv.EMBALATJE
                    oEmbalatje = oItm.Amt
                Case MaxiSrvr.spv.TRANSPORT
                    oTransport = oItm.Amt
            End Select
        Next
    End Sub

    Private Sub Xl_Contact1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Contact1.AfterUpdate
        If mAllowevents Then
            SetDirty()
        End If
    End Sub

    Private Sub SetGrid(ByVal intYea As Integer, ByVal LngId As Long)
        Dim SQL As String = "SELECT SPV.YEA,SPV.ID,SPVART.NOM,CLX FROM SPV INNER JOIN " _
        & "CLX ON SPV.EMP=CLX.EMP AND SPV.CLI=CLX.CLI INNER JOIN " _
        & "SPVART ON SPV.EMP=SPVART.EMP AND SPV.ART=SPVART.ID " _
        & "WHERE SPV.EMP=" & mEmp.Id & " AND " _
        & "SPVINYEA=" & intYea & " AND " _
        & "SPVINID=" & LngId & " " _
        & "ORDER BY SPV.YEA,SPV.ID"
        mDsSpvinSpvs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsSpvinSpvs.Tables(0)

        With DataGridViewSpvInSpvs
            With .RowTemplate
                .Height = DataGridViewSpvInSpvs.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .Width = 50
            End With
            With .Columns(Cols.ArtNom)
                .Width = 70
            End With
            With .Columns(Cols.Clx)
                .Width = 50
            End With
        End With
    End Sub

    Private Function CurrentSpvinSpv() As spv
        Dim oSpv As spv = Nothing
        Dim oRow As DataGridViewRow = DataGridViewSpvInSpvs.CurrentRow
        If oRow IsNot Nothing Then
            oSpv = spv.FromId(mEmp, oRow.Cells(Cols.Yea).Value, oRow.Cells(Cols.Id).Value)
        End If
        Return oSpv
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oSpv As spv = CurrentSpvinSpv()

        If oSpv IsNot Nothing Then
            Dim oMenu_Spv As New Menu_Spv(oSpv)
            AddHandler oMenu_Spv.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Spv.Range)
        End If

        DataGridViewSpvInSpvs.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Id
        Dim oGrid As DataGridView = DataGridViewSpvInSpvs

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        Refresca()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonShowAlb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonShowAlb.Click
        root.ShowAlb(mSpv.Alb)
    End Sub

    Private Sub SetDirty()
        ButtonOk.Enabled = True
    End Sub


    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        With mSpv
            Dim BlNewSpv As Boolean = (.Id = 0)
            If CheckBoxAdr.Checked Then
                .Nom = TextBoxNom.Text
                .Adr = Xl_Adr1.Adr
            Else
                .Nom = ""
                .Adr = Nothing
            End If
            If mProduct IsNot Nothing Then
                .Product = mProduct
            Else
                .Art = mSpvArt
            End If
            .Client = New Client(Xl_Contact1.Contact.Guid)
            .SolicitaGarantia = CheckBoxSolicitaGarantia.Checked
            .ValJob = Xl_AmtSpvJob.Amt
            .ValMaterial = Xl_AmtSpvMaterial.Amt
            .ValEmbalatje = Xl_AmtSpvEmbalatje.Amt
            .ValPorts = Xl_AmtSpvTransport.Amt
            .Garantia = CheckBoxGarantiaConfirmada.Checked
            .CliObs = TextBoxCliObs.Text
            .Contacto = TextBoxContacto.Text
            .ObsTecnic = TextBoxObsTecnic.Text
            .sRef = TextBoxsRef.Text

            If BlNewSpv Then
                If ComboBoxEmailLabelTo.SelectedIndex >= 0 Then
                    Dim s As String = ComboBoxEmailLabelTo.SelectedItem.ToString
                    Do While s = "" Or s = "(altres)"
                        s = InputBox("e-mail:", "ENVIAMENT DE CORREO REPARACIO")
                    Loop
                    .LabelEmailedTo = s
                End If

                Dim oContact As DTOContact = BLL.BLLSession.Current.Contact
                If oContact Is Nothing Then
                    Dim oUser As DTOUser = BLL.BLLSession.Current.User
                    Dim oContacts As List(Of DTOContact) = BLL.BLLUser.Contacts(oUser)
                    If oContacts.Count > 0 Then
                        oContact = oContacts(0)
                    End If
                End If

                If oContact IsNot Nothing Then
                    .UsrRegister = New Contact(oContact.Guid)
                End If
            End If

            If CheckBoxRead.Checked Then
                If .FchRead > Date.MinValue Then
                Else
                    .FchRead = Today
                End If
            Else
                .FchRead = Date.MinValue
            End If

            If mDirtyOutSpvIn Then
                .UsrOutOfSpvIn = root.Usuari
                .FchOutOfSpvIn = Now
                If CheckBoxOutSpvIn.Checked Then
                    .SpvIn = New SpvIn(mEmp, -1, -1)
                    .ObsOutOfSpvIn = TextBoxObsOutOfSpvIn.Text
                Else
                    .SpvIn = New SpvIn(mEmp)
                End If
            End If
            If mDirtyOutSpvOut Then
                If CheckBoxOutSpvOut.Checked Then
                    .Alb = Nothing
                    .UsrOutOfSpvOut = root.Usuari
                    .FchOutOfSpvOut = Now
                    .ObsOutOfSpvOut = TextBoxObsOutOfSpvOut.Text
                Else
                    .UsrOutOfSpvOut = Nothing
                    .FchOutOfSpvOut = Nothing
                    .ObsOutOfSpvOut = ""
                    .Alb = Nothing
                End If
            End If
            If mDirtyAlb Then
                .ValJob = Xl_AmtJob.Amt
                .ValMaterial = Xl_AmtMaterial.Amt
                .ValEmbalatje = Xl_AmtEmbalatje.Amt
                .ValPorts = Xl_AmtTransport.Amt
                If .Alb Is Nothing Then
                    Dim oEmp as DTOEmp = BLL.BLLApp.Emp
                    Dim oTaller As Taller = App.Current.emp.DefaultTaller
                    Dim oMgz As New Mgz(oTaller.Guid)
                    Dim oAlb As New Alb(oEmp, DTOPurchaseOrder.Codis.reparacio, oMgz)
                    With oAlb
                        .Client = New Client(Xl_Contact1.Contact.Guid)
                        .Fch = Today
                        .Nom = .Client.Nom_o_NomComercial
                        .Adr = .Client.DeliveryAdr.Text
                        .Zip = .DeliveryZip
                        .Tel = .Client.Tel
                        If CheckBoxFacturable.Checked Then
                            .Facturable = True
                            .CashCod = .Client.CashCod
                        End If
                        SetAmtsToAlb()
                        .SetUser(BLL.BLLSession.Current.User)

                        exs = New List(Of exception)
                        If Not .Update(exs) Then
                            MsgBox(BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                        End If
                    End With
                    .Alb = oAlb
                End If
            End If

            exs = New List(Of exception)
            If .Update( exs) Then
                If mIncidencia IsNot Nothing Then
                    mIncidencia.Description = mIncidencia.Description & vbCrLf & "reparacion " & .Id
                    mIncidencia.Spv = New DTOSpv(mSpv.Guid)
                    exs = New List(Of exception)
                    If Not BLL_Incidencia.Update(mIncidencia, exs) Then
                        MsgBox("error al desar la reparació" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                    End If
                End If

                RaiseEvent AfterUpdate(sender, e)
                If BlNewSpv Then
                    MsgBox("nova reparació " & .Id, MsgBoxStyle.Information, "MAT.NET")
                    Dim oMailMessage As MailMessage = Nothing
                    If mSpv.MailMessage(oMailMessage, exs) Then
                        If Not MatOutlook.NewMessage(oMailMessage, exs) Then
                            UIHelper.WarnError( exs, "error al enviar el missatge")
                        End If
                    Else
                        UIHelper.WarnError( exs, "error al redactar el missatge")
                    End If
                End If

                Me.Close()

            Else
                MsgBox("error al desar la reparació" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If

        End With
    End Sub

    Private Sub CheckBoxOutSpvIn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxOutSpvIn.CheckedChanged
        If mAllowevents Then
            mDirtyOutSpvIn = True
            TextBoxObsOutOfSpvIn.Visible = CheckBoxOutSpvIn.Checked
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxOutSpvOut_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxOutSpvOut.CheckedChanged
        If mAllowevents Then
            mDirtyOutSpvOut = True
            TextBoxObsOutOfSpvOut.Visible = CheckBoxOutSpvOut.Checked
            SetDirty()
        End If
    End Sub


    Private Sub CheckBoxFacturable_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    CheckBoxFacturable.CheckedChanged
        If mAllowevents Then
            mDirtyAlb = True
            SetDirty()
        End If
    End Sub

    Private Sub Pressupost_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtSpvEmbalatje.AfterUpdate, Xl_AmtSpvTransport.AfterUpdate, Xl_AmtSpvMaterial.AfterUpdate, Xl_AmtSpvJob.AfterUpdate
        If mAllowevents Then
            SetDirty()
        End If
    End Sub

    Private Sub Xl_Amt_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtTransport.AfterUpdate, Xl_AmtEmbalatje.AfterUpdate, Xl_AmtMaterial.AfterUpdate, Xl_AmtJob.AfterUpdate
        If mAllowevents Then
            mDirtyAlb = True
            SetDirty()
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la reparació num." & mSpv.Id & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mSpv.Delete( exs) Then
                MsgBox("Reparació num." & mSpv.Id & " eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                MsgBox("Operació cancelada." & vbCrLf & "La reparació ja ha sortit." & vbCrLf & "Cal eliminar primer l'albará", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub CheckBoxSolicitaGarantia_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    CheckBoxSolicitaGarantia.CheckedChanged, _
    CheckBoxGarantiaConfirmada.CheckedChanged, _
    TextBoxCliObs.TextChanged, _
    TextBoxsRef.TextChanged, _
    TextBoxContacto.TextChanged
        If mAllowevents Then
            SetDirty()
        End If
    End Sub



    Private Sub DataGridViewSpvInSpvs_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewSpvInSpvs.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub CheckBoxAdr_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAdr.CheckedChanged
        TextBoxNom.Enabled = CheckBoxAdr.Checked
        Xl_Adr1.Enabled = CheckBoxAdr.Checked
        SetDirty()
        If CheckBoxAdr.Checked And TextBoxNom.Text = "" And Xl_Adr1.Adr.Zip Is Nothing Then
            TextBoxNom.Text = mSpv.Client.Nom_o_NomComercial
            Xl_Adr1.Adr = mSpv.Client.DeliveryAdr
        End If
    End Sub

    Private Sub CheckBoxRead_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxRead.CheckedChanged
        If mAllowevents Then
            SetDirty()
        End If
    End Sub

    Private Sub ButtonNoSpvIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNoSpvIn.Click
        mSpv.SpvIn = New SpvIn(mSpv.Emp, 0, 0)
        SetDirty()
        ButtonNoSpvIn.Enabled = False
    End Sub




    Private Sub Xl_Product1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Product1.AfterUpdate
        mProduct = sender
        SetDirty()
    End Sub

    Private Sub TextBoxObsTecnic_TextChanged(sender As Object, e As EventArgs) Handles _
        TextBoxObsTecnic.TextChanged
        If mAllowevents Then
            mDirtyAlb = True
            SetDirty()
        End If
    End Sub


End Class
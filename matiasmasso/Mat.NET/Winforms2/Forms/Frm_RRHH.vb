Public Class Frm_RRHH
    Private _Staffs As List(Of DTOStaff)
    Private _Done(10)
    Private _allowEvents As Boolean

    Private _QrPerFichar As String = "https://bit.ly/3ry6WqX"

    Private Enum Tabs
        Staffs
        Nomines
        CertificatsIrpf
        RegistresJornadaLaboral
        Mods145
    End Enum

    Private Async Sub Frm_RRHH_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadYears()
        ImportarCertificatsIrpfToolStripMenuItem.Text = String.Format("importar Certificats Irpf {0}", DTO.GlobalVariables.Today().Year - 1)
        Await refrescaStaffs()
    End Sub


    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Nomines
                If Not _Done(Tabs.Nomines) Then
                    Await refrescaNominas()
                    _Done(Tabs.Nomines) = True
                End If
            Case Tabs.CertificatsIrpf
                If Not _Done(Tabs.CertificatsIrpf) Then
                    Await refrescaCertificatsIrpf()
                    _Done(Tabs.CertificatsIrpf) = True
                End If
            Case Tabs.RegistresJornadaLaboral
                If Not _Done(Tabs.RegistresJornadaLaboral) Then
                    Await refrescaRegistresJornadaLaboral()
                    _Done(Tabs.RegistresJornadaLaboral) = True
                End If
            Case Tabs.Mods145
                If Not _Done(Tabs.Mods145) Then
                    Await refrescaMods145()
                    _Done(Tabs.Mods145) = True
                End If
        End Select
    End Sub

    Private Sub LoadYears()
        For i = DTO.GlobalVariables.Today().Year To 1985 Step -1
            ComboBoxYears.Items.Add(i)
            ComboBoxYearNominas.Items.Add(i)
        Next
        ComboBoxYears.SelectedIndex = 0
        ComboBoxYearNominas.SelectedIndex = 0
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Staffs1.Filter = e.Argument
        Xl_Nominas1.Filter = e.Argument
        Xl_CertificatsIrpf1.Filter = e.Argument
        Xl_Mods1451.Filter = e.Argument
    End Sub


#Region "StaffList"

    Private Async Function refrescaStaffs() As Task
        Dim exs As New List(Of Exception)
        _allowEvents = False
        ProgressBar1.Visible = True
        _Staffs = Await FEB.Staffs.All(exs, GlobalVariables.Emp)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Staffs1.Load(FilteredStaffs())
        Else
            UIHelper.WarnError(exs)
        End If
        _allowEvents = True
    End Function


    Private Function FilteredStaffs() As List(Of DTOStaff)
        Dim retval = _Staffs.Where(Function(x) IsActive(x)).ToList
        Return retval
    End Function

    Private Function IsActive(oStaff As DTOStaff) As Boolean
        Dim retval As Boolean
        If CheckBoxYear.Checked Then
            Dim isAlta = oStaff.alta.Year <= CurrentStaffsYear()
            Dim isBaixa = oStaff.baixa <> Nothing AndAlso oStaff.baixa.Year < CurrentStaffsYear()
            retval = isAlta And Not isBaixa
        Else
            retval = True
        End If
        Return retval
    End Function

    Private Function CurrentStaffsYear() As Integer
        Return ComboBoxYears.SelectedItem
    End Function

    Private Async Sub CheckBoxYear_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxYear.CheckedChanged
        Await refrescaStaffs()
    End Sub

    Private Async Sub ComboBoxYears_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxYears.SelectedIndexChanged
        Await refrescaStaffs()
    End Sub

#End Region

#Region "Nomines"
    Private Async Sub ComboBoxYearNominas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxYearNominas.SelectedIndexChanged
        If _allowEvents Then
            Await refrescaNominas()
        End If
    End Sub

    Private Async Sub refrescaNominas(sender As Object, e As MatEventArgs)
        Await refrescaNominas()
    End Sub

    Private Async Function refrescaNominas() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oNominas = Await FEB.Nominas.All(exs, New DTOExercici(GlobalVariables.Emp, ComboBoxYearNominas.SelectedItem))
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Nominas1.Load(oNominas)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Sub ImportarNominesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarNominesToolStripMenuItem.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "fitxers pdf|*.pdf"
            If .ShowDialog Then
                Dim sFilename = .FileName
                Dim oFrm As New Frm_NominasFactory(sFilename)
                AddHandler oFrm.AfterUpdate, AddressOf refrescaNominas
                oFrm.Show()
            End If
        End With
    End Sub

    Private Sub Xl_Nominas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Nominas1.RequestToAddNew
        Dim oFrm As New Frm_NominaFactory()
        AddHandler oFrm.AfterUpdate, AddressOf refrescaNominas
        oFrm.Show()
    End Sub


#End Region

#Region "Certificats"

    Private Async Function refrescaCertificatsIrpf() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim items = Await FEB.CertificatIrpfs.All(exs, GlobalVariables.Emp)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_CertificatsIrpf1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub ImportarCertificatsIrpfToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarCertificatsIrpfToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog()
        With oDlg
            .Filter = "documents pdf|*.pdf|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                Try
                    Dim sfilenames = LegacyHelper.iTextPdfHelper.splitFileIntoPages(exs, .FileName)
                    If exs.Count = 0 Then
                        ProgressBar1.Visible = True

                        Dim oCert = Await FEB.Cert.Find(GlobalVariables.Emp.Org, exs)
                        If exs.Count = 0 Then
                            If oCert Is Nothing Then Throw New Exception("Falta el certificat d'emporesa per la signatura digital")

                            Dim sSignedFilenames As New List(Of String)
                            Dim rect As Rectangle = New Rectangle(300, 32, 200, 75)

                            For Each sFilename In sfilenames
                                Dim sSignedFilename = sFilename.Substring(0, sFilename.LastIndexOf(".pdf")) & ".signed.pdf"
                                If LegacyHelper.PdfSignatureHelper.Sign(exs, sFilename, sSignedFilename, oCert.memoryStream, oCert.Pwd, rect, oCert.ImageUri) Then
                                    sSignedFilenames.Add(sSignedFilename)
                                Else
                                    exs.Add(New Exception(String.Format("Error al signar el fitxer {0}", sFilename)))
                                End If
                                Application.DoEvents()
                            Next

                            'Dim oCertificats = DTOCertificatIrpf.Factory(exs, DTO.GlobalVariables.Today().Year - 1, sSignedFilenames)
                            Dim oCertificats = CertificatsList(exs, DTO.GlobalVariables.Today().Year - 1, sSignedFilenames)
                            For Each item In oCertificats
                                item.Contact = Await FEB.Contact.FromNif(exs, GlobalVariables.Emp, item.Nif)
                                If item.Contact Is Nothing Then
                                    exs.Add(New Exception("No s'ha trobat cap contacte amb el nif '" & item.Nif & "' de '" & item.Nom & "'"))
                                Else
                                    item.Nom = item.Contact.Nom
                                    item.DocFile = LegacyHelper.DocfileHelper.Factory(item.filename, exs)
                                    If exs.Count > 0 Then
                                        exs.Add(New Exception("No s'ha trobat el fitxer '" & item.filename & "'"))
                                    End If
                                End If
                            Next
                            For Each item In oCertificats
                                Await FEB.CertificatIrpf.Upload(exs, item)
                            Next
                            Await refrescaCertificatsIrpf()

                        End If
                    End If

                Catch ex As Exception
                    exs.Add(ex)
                End Try

                ProgressBar1.Visible = False
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs)
                End If

            End If
        End With
    End Sub

    Public Shared Function CertificatsList(ByVal exs As List(Of Exception), ByVal year As Integer, ByVal filenames As List(Of String)) As List(Of DTOCertificatIrpf)
        Dim retval As List(Of DTOCertificatIrpf) = New List(Of DTOCertificatIrpf)()

        For Each sfilename In filenames
            Dim item = DTOCertificatIrpf.Factory(sfilename)
            Dim src = LegacyHelper.iTextPdfHelper.readText(sfilename, exs)

            If exs.Count = 0 Then
                Dim lines = src.Split(vbLf).ToList()
                Dim keyLine = lines.FirstOrDefault(Function(x) x.Contains("Datos del Perceptor"))

                If Not String.IsNullOrEmpty(keyLine) Then
                    Dim idx = lines.IndexOf(keyLine)

                    If lines.Count() > idx + 3 Then
                        Dim sLine = lines(idx + 2)
                        Dim iPos = sLine.IndexOf(" ")
                        item.Nif = sLine.Substring(0, iPos)
                        If item.Nif.StartsWith("0") Then item.Nif = item.Nif.Substring(1)
                        item.Nom = sLine.Substring(iPos + 1)
                        item.Year = year
                        retval.Add(item)
                    End If
                End If
            Else
                exs.Add(New Exception("error al llegir '" & sfilename & "'"))
            End If
        Next

        Return retval
    End Function

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.RegistresJornadaLaboral
                Dim oBook = Xl_RegistresJornadesLaborals1.Excel
                If Not UIHelper.ShowExcel(oBook, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Case Else
                Dim oSheet = DTOStaff.excel(FilteredStaffs, Current.Session.Lang)
                If Not UIHelper.ShowExcel(oSheet, exs) Then
                    UIHelper.WarnError(exs)
                End If
        End Select
    End Sub

    Private Async Sub Xl_CertificatsIrpf1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CertificatsIrpf1.RequestToRefresh
        Await refrescaCertificatsIrpf()
    End Sub

    Private Sub Xl_CertificatsIrpf1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_CertificatsIrpf1.RequestToToggleProgressBar
        Dim visible As Boolean = e.Argument
        ProgressBar1.Visible = visible
    End Sub


#End Region

#Region "RegistresJornadaLaboral"
    Private Async Function refrescaRegistresJornadaLaboral() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim value = Await FEB.JornadesLaborals.All(exs)
        If exs.Count = 0 Then
            Xl_RegistresJornadesLaborals1.Load(value)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_RegistresJornadesLaborals1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RegistresJornadesLaborals1.RequestToRefresh
        Await refrescaRegistresJornadaLaboral()
    End Sub



#End Region

#Region "Mods145"
    Private Async Sub RefrescaMods145(sender As Object, e As EventArgs)
        Await RefrescaMods145()
    End Sub
    Private Async Function RefrescaMods145() As Task
        Dim exs As New List(Of Exception)
        _allowEvents = False
        ProgressBar1.Visible = True
        Dim oMods145 = Await FEB.Mods145.GetValues(exs, GlobalVariables.Emp)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Mods1451.Load(oMods145)
        Else
            UIHelper.WarnError(exs)
        End If
        _allowEvents = True
    End Function

    Private Sub Xl_Mods1451_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Mods1451.RequestToAddNew
        Dim oDoc As New DTOContactDoc()
        oDoc.Type = DTOContactDoc.Types.Model_145
        oDoc.Fch = DateTime.Today
        oDoc.Ref = "Mod.145 - " & oDoc.Fch.Year.ToString()
        Dim oFrm As New Frm_ContactDoc(oDoc)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaMods145
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Mods1451_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Mods1451.RequestToRefresh
        Await RefrescaMods145()
    End Sub

#End Region

End Class
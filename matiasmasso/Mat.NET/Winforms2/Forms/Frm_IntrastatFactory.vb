Public Class Frm_IntrastatFactory
    Private _Intrastat As DTOIntrastat
    Private _dirtyIntrastatUrl As Boolean
    Private _fromIntrastatList As Boolean
    Private _allowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Intro
        Partides
        Skus
        Registre
    End Enum

    Private Enum Images
        Empty
        Warn
    End Enum

    Public Sub New()
        MyBase.New
        InitializeComponent()
        _fromIntrastatList = True
    End Sub

    Public Sub New(oYearMonth As DTOYearMonth, oFlujo As DTOIntrastat.Flujos, Optional fromIntrastatList As Boolean = False)
        MyBase.New
        InitializeComponent()
        _fromIntrastatList = fromIntrastatList

        Xl_YearMonth1.YearMonth = oYearMonth
        Select Case oFlujo
            Case DTOIntrastat.Flujos.Introduccion
                RadioButtonImport.Checked = True
            Case DTOIntrastat.Flujos.Expedicion
                RadioButtonExport.Checked = True
        End Select
        TabControl1.SelectedIndex = Tabs.Partides
    End Sub

    Private Async Sub Frm_IntrastatFactory_Load(sender As Object, e As EventArgs) Handles Me.Load
        If TabControl1.SelectedIndex = Tabs.Partides Then
            Await refresca()
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim sFluxe As String = IIf(CurrentFlujo() = DTOIntrastat.Flujos.Introduccion, "Introduccions", "Espedicions")
        Dim sYear As String = Xl_YearMonth1.YearMonth.Year
        Dim sMonth As String = DTOLang.CAT.MesAbr(Xl_YearMonth1.YearMonth.Month)
        Me.Text = String.Format("Nou Intrastat de {0} {1} {2}", sFluxe, sYear, sMonth)

        ProgressBar1.Visible = True
        PanelWizardPartides.Visible = False
        Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Dim exs As New List(Of Exception)
        _Intrastat = Await FEB.Intrastat.Factory(Current.Session.Emp, CurrentFlujo, Xl_YearMonth1.YearMonth, exs)
        _Intrastat.partidas = _Intrastat.partidas.Where(Function(x) x.importeFacturado > 0).ToList 'No s'admeten partides negatives
        TextBoxUrlIntrastat.Text = Await FEB.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.urlIntrastat, exs)
        If exs.Count = 0 Then
            Xl_IntrastatPartidas1.Load(_Intrastat)
            Xl_IntrastatSkus1.Load(_Intrastat)

            If Xl_IntrastatPartidas1.Warn Then
                TabControl1.TabPages(Tabs.Partides).ImageIndex = Images.Warn
            Else
                TabControl1.TabPages(Tabs.Partides).ImageIndex = -1
            End If

            If Xl_IntrastatSkus1.Warn Then
                TabControl1.TabPages(Tabs.Skus).ImageIndex = Images.Warn
            Else
                TabControl1.TabPages(Tabs.Skus).ImageIndex = -1
            End If

            Cursor = Cursors.Default
            PanelWizardPartides.Visible = True
            ProgressBar1.Visible = False
        Else
            Cursor = Cursors.Default
            PanelWizardPartides.Visible = True
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Function CurrentFlujo() As DTOIntrastat.Flujos
        Dim retval As DTOIntrastat.Flujos
        If RadioButtonImport.Checked Then
            retval = DTOIntrastat.Flujos.Introduccion
        ElseIf RadioButtonExport.Checked Then
            retval = DTOIntrastat.Flujos.Expedicion
        End If
        Return retval
    End Function


    Private Sub Xl_IntrastatSkus1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_IntrastatSkus1.RequestToRefresh
        Xl_IntrastatPartidas1.Load(_Intrastat)
        Xl_IntrastatSkus1.Load(_Intrastat)
    End Sub


    Private Async Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        ProgressBar1.Visible = True
        PanelWizardPartides.Visible = False
        Application.DoEvents()
        Dim exs As New List(Of Exception)
        If Await FEB.Intrastat.Update(_Intrastat, exs) Then
            TabControl1.SelectedIndex = Tabs.Registre
            PanelWizardPartides.Visible = True
            ProgressBar1.Visible = False
        Else
            PanelWizardPartides.Visible = True
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oBook = DTOIntrastat.ExcelExport(_Intrastat)
        If Not UIHelper.ShowExcel(oBook, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub ButtonSaveFile_Click(sender As Object, e As EventArgs) Handles ButtonSaveFile.Click
        Dim sFolderpath As String = "C:\AEAT"
        Dim exs As New List(Of Exception)
        If MatHelperStd.IOHelper.CheckOrCreateFolder(sFolderpath, exs) Then
            Dim oDlg As New SaveFileDialog
            With oDlg
                .InitialDirectory = "C:\AEAT"
                .Title = "Desar fitxer Intrastat"
                .Filter = "fitxers Txt|*.txt|tots els arxius|*.*"
                .FilterIndex = 0
                .FileName = DTOIntrastat.DefaultFileName(_Intrastat)
                .AddExtension = True
                .DefaultExt = ".txt"

                If .ShowDialog = DialogResult.OK Then
                    Dim sr As IO.StreamWriter
                    Try
                        Dim src As String = DTOIntrastat.FileStringBuilder(_Intrastat)
                        sr = New IO.StreamWriter(.FileName, False, System.Text.Encoding.Default)
                        sr.Write(src)
                        sr.Flush()
                        sr.Close()
                    Catch ex As Exception
                        UIHelper.WarnError(ex)
                    End Try
                End If
            End With
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub



    Private Sub ButtonBrowse_Click(sender As Object, e As EventArgs) Handles ButtonBrowse.Click
        Dim url As String = TextBoxUrlIntrastat.Text
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub TextBoxUrlIntrastat_TextChanged(sender As Object, e As EventArgs) Handles TextBoxUrlIntrastat.TextChanged
        If _allowEvents Then
            _dirtyIntrastatUrl = True
        End If
    End Sub

    Private Sub ButtonUpload_Click(sender As Object, e As EventArgs) Handles ButtonUpload.Click
        If UIHelper.LoadPdfDialog(_Intrastat.DocFile, "pujar justificant declaració Intrastat") Then
            TextBoxPdfFilename.Text = _Intrastat.DocFile.filename
            PictureBoxDocfile.Image = LegacyHelper.ImageHelper.Converter(_Intrastat.DocFile.Thumbnail)
        End If
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_Exit.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtonsRegistre, True)
        If _dirtyIntrastatUrl Then
            If Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.urlIntrastat, TextBoxUrlIntrastat.Text, exs) Then
            Else
                UIHelper.ToggleProggressBar(PanelButtonsRegistre, False)
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If

        _Intrastat.Csv = TextBox2.Text

        If Await FEB.Intrastat.Update(_Intrastat, exs) Then
            If _fromIntrastatList Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Intrastat))
            Else
                Dim oFrm As New Frm_Intrastats
                oFrm.Show()
            End If
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtonsRegistre, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonIntroNext_Click(sender As Object, e As EventArgs) Handles ButtonIntroNext.Click
        Await refresca()
        Cursor = Cursors.Default
        TabControl1.SelectedIndex = Tabs.Partides
    End Sub

    Private Sub CheckBoxFilterSkuWarnings_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFilterSkuWarnings.CheckedChanged
        Xl_IntrastatSkus1.FilterWarnings(CheckBoxFilterSkuWarnings.Checked)
    End Sub
End Class
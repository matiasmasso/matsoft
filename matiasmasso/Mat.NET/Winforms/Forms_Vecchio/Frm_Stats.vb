Imports System.ComponentModel

Public Class Frm_Stats
    Private _Stat As DTOStat
    Private _AllowEvents As Boolean

    Private _BackGroundWorker As BackgroundWorker

    Public Sub New(Optional oStat As DTOStat = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        If oStat Is Nothing Then
            oStat = New DTOStat(DTOStat.ConceptTypes.Product, BLLApp.Lang)
        End If
        _Stat = oStat
    End Sub

    Private Sub Frm_Stats_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadConcepts()
        LoadCnaps()
        LoadYears()
        _Stat.Year = Xl_Years1.Value
        LoadReps()
        LoadProveidors()
        LoadAreas()
        LoadProducts()
        ComboBoxUnits.SelectedIndex = 0
        'BLL.BLLStat.Load2(_Stat)

        refresca()
    End Sub

    Private Sub BackGroundLoader(sender As Object, e As DoWorkEventArgs)
        'to deprecate
        BLL.BLLStat.Load2(_Stat)
    End Sub

    Private Sub BackGroundLoaded(sender As Object, e As RunWorkerCompletedEventArgs)
        'to deprecate
        Xl_Stats1.Load(_Stat)
        Cursor = Cursors.Default
        PictureBoxWait.Visible = False
        Application.DoEvents()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        PictureBoxWait.Visible = True
        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        With _Stat
            .ConceptType = ComboBoxConcept.SelectedIndex
            .Year = Xl_Years1.Value
            .Rep = Xl_RepsCombo1.Value
            .Area = Xl_LookupArea1.Area
            .Product = Xl_LookupProduct1.Product
            .DistributionChannel = Xl_LookupDistributionChannel1.DistributionChannel
            .Format = ComboBoxUnits.SelectedIndex ' DTOStat.Formats.Amounts, DTOStat.Formats.Units
            .IncludeHidden = CheckBoxIncludeHidden.Checked
            .GroupByHolding = CheckBoxGroupByHolding.Checked

            Dim oGuidNom As DTOGuidNom = Xl_GuidNoms_Proveidors.Value
            If oGuidNom Is Nothing Then
                .Proveidor = Nothing
            Else
                .Proveidor = New DTOProveidor(oGuidNom.Guid)
                .Proveidor.Nom = oGuidNom.Nom
            End If
        End With

        Xl_Years1.Visible = _Stat.ConceptType <> DTOStat.ConceptTypes.Yeas
        BLL.BLLStat.Load2(_Stat)

        Xl_Stats1.Load(_Stat)
        Cursor = Cursors.Default
        PictureBoxWait.Visible = False
        'Application.DoEvents()
        _AllowEvents = True


        '_BackGroundWorker = New BackgroundWorker
        '_BackGroundWorker.WorkerReportsProgress = True
        'AddHandler _BackGroundWorker.DoWork, AddressOf BackGroundLoader
        'AddHandler _BackGroundWorker.RunWorkerCompleted, AddressOf BackGroundLoaded
        '_BackGroundWorker.RunWorkerAsync()
    End Sub

    Private Sub LoadConcepts()
        ComboBoxConcept.DataSource = BLL.BLLStat.Conceptes(_Stat)
        ComboBoxConcept.SelectedIndex = _Stat.ConceptType
    End Sub

    Private Sub LoadYears()
        Dim oYears As List(Of Integer) = BLL.BLLStat.Years(BLL.BLLSession.Current.User)
        Xl_Years1.Load(oYears)
    End Sub

    Private Sub LoadCnaps()
        'Dim oCnaps As List(Of Cnap) = StatLoader.Cnaps(_Stat)
        'Xl_CnapsCombo1.Load(oCnaps, _Stat.Cnap)
    End Sub

    Private Sub LoadProveidors()
        Dim oProveidors As List(Of DTOGuidNom) = BLL.BLLStat.Proveidors(_Stat)
        Dim oGuid As Guid = Nothing
        If _Stat.Proveidor IsNot Nothing Then
            oGuid = _Stat.Proveidor.Guid
        End If
        Xl_GuidNoms_Proveidors.load(oProveidors, oGuid, BLL.BLLSession.Current.Lang.Tradueix("(todos los proveedores)", "(tots els proveïdors)", "(any supplier)"))
    End Sub

    Private Sub LoadReps()
        Dim oReps As List(Of DTORep) = BLL.BLLStat.Reps(Xl_Years1.Value)
        Xl_RepsCombo1.Load(oReps, _Stat.Rep)
    End Sub

    Private Sub LoadAreas()
        'Dim oAtlas As DTOAtlas = BLL.BLLStat.Atlas(_Stat)
        Xl_LookupArea1.Area = _Stat.Area
    End Sub

    Private Sub LoadProducts()
        'Dim oCatalog As DTOProductCatalog = BLL.BLLStat.Catalog(_Stat)
        Xl_LookupProduct1.Product = _Stat.Product
    End Sub

    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        LoadReps()
        LoadAreas()
        refresca()
    End Sub

    Private Sub Xl_RepsCombo1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_RepsCombo1.AfterUpdate
        LoadAreas()
        refresca()
    End Sub

    Private Sub ComboBoxConcept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxConcept.SelectedIndexChanged
        If _AllowEvents Then
            Select Case ComboBoxConcept.SelectedIndex
                Case DTOStat.ConceptTypes.Yeas
                    Dim oReps As List(Of DTORep) = BLL.BLLStat.Reps()
                    Xl_RepsCombo1.Load(oReps, _Stat.Rep)
                Case Else
                    If _Stat.ConceptType = DTOStat.ConceptTypes.Yeas Then
                        Dim oReps As List(Of DTORep) = BLL.BLLStat.Reps(Xl_Years1.Value)
                        Xl_RepsCombo1.Load(oReps, _Stat.Rep)
                    End If
            End Select

            refresca()
        End If
    End Sub

    Private Sub Xl_CnapsCombo1_AfterUpdate(sender As Object, e As MatEventArgs)
        LoadAreas()
        refresca()
    End Sub

    Private Sub ControlValueChanged(sender As Object, e As EventArgs) Handles _
            Xl_GuidNoms_Proveidors.AfterUpdate,
        Xl_LookupArea1.AfterUpdate,
         Xl_LookupProduct1.AfterUpdate,
          Xl_LookupDistributionChannel1.AfterUpdate,
           ComboBoxUnits.SelectedIndexChanged,
            CheckBoxIncludeHidden.CheckedChanged,
             CheckBoxGroupByHolding.CheckedChanged

        If _AllowEvents Then
            refresca()
        End If
    End Sub



    Private Sub PictureBoxExcel_Click(sender As Object, e As EventArgs) Handles PictureBoxExcel.Click
        Dim oSheet As DTOExcelSheet = BLL.BLLStat.ExcelSheet(_Stat)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class


Public Class Wz_Girs
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mCsbs As Csbs
    Private mDsCsas As DataSet
    Private mDirtyEfts As Boolean
    Private mAllowEvents As Boolean
    Private mCsaType As DTOCsa.Types

    Private Sub Wz_Girs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadEfectes()
        mAllowEvents = True
    End Sub

    Private Sub LoadEfectes()
        Dim oMode As Xl_Gir_SelEfts.Modes = CurrentSepaMode()
        Xl_Gir_SelEfts1.LoadData(New Country("ES"), oMode)
    End Sub

    Public WriteOnly Property OnlyNoDomiciliats() As Boolean
        Set(ByVal value As Boolean)
            CheckBoxNoDomiciliats.Checked = value
        End Set
    End Property

    Public WriteOnly Property CsaType() As DTO.DTOCsa.Types
        Set(ByVal value As DTO.DTOCsa.Types)
            mCsaType = value
            Select Case mCsaType
                Case DTOCsa.Types.AlCobro
                    Me.Text = "REMESA DE EFECTES AL COBRAMENT"
                Case DTOCsa.Types.AlDescompte
                    Me.Text = "REMESA DE EFECTES AL DESCOMPTE"
            End Select
        End Set
    End Property

    Private Sub Grabacio()
        Dim MaxId As Integer
        Dim MinId As Integer
        Dim s As String
        Dim oCsa As Csa
        Dim oCsas As Csas = Xl_Gir_SelBancs1.Csas
        For Each oCsa In oCsas
            With oCsa
                .descomptat = (mCsaType = DTO.DTOCsa.Types.AlDescompte)
                If .csbs.Count > 0 Then
                    .csbs.SortByClient()
                    Dim exs as New List(Of exception)
                    If Not .Update( exs) Then
                        MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                    End If
                    If MinId = 0 Then MinId = .Id
                    MaxId = .Id
                End If
            End With
        Next
        If MaxId = MinId Then
            s = "remesa num." & MinId
        Else
            s = "remeses " & MinId & "-" & MaxId
        End If
        MsgBox(s, MsgBoxStyle.Information, "MAT.NET")
        Me.Close()
    End Sub

    Private Sub LoadSelectedCsbs()
        Dim oFileFormat As DTOCsa.FileFormats = IIf(CurrentSepaMode() = Xl_Gir_SelEfts.Modes.SEPAB2B, DTOCsa.FileFormats.SepaB2b, DTOCsa.FileFormats.Norma58)
        Xl_Gir_SelBancs1.LoadCsbs(Xl_Gir_SelEfts1.Csbs, oFileFormat)
        mDirtyEfts = False
    End Sub

    Private Sub Xl_Gir_SelEfts1_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Gir_SelEfts1.Changed
        mDirtyEfts = True
    End Sub

#Region "Wizard Common Events"
    Private Sub Wizard_AfterTabSelect()
        EnableNavButtons()
        Dim oTab As TabPage = TabControl1.SelectedTab
        Select Case oTab.Text
            Case TabPageCsas.Text
                If mDirtyEfts Then
                    LoadSelectedCsbs()
                End If
        End Select
    End Sub


    Private Sub Wizard_NavigateNext(ByVal oPageSource As TabPage, Optional ByRef oPageTarget As TabPage = Nothing)
        Select Case oPageSource.Text
            Case TabPageCsbs.Text
        End Select
    End Sub

    Private Sub Wizard_NavigatePrevious(ByVal oPageSource As TabPage, Optional ByRef oPageTarget As TabPage = Nothing)
        Select Case oPageSource.Text
            'Case TabPageBancs.Text, TabPageVisas.Text
            'TabControl1.SelectedTab = TabPageCfp
            'Case TabPageEnd.Text
            'oPageTarget = CfpTabPage()
        End Select
    End Sub

    Private Sub Wizard_NavigateEnd()
        Grabacio()
    End Sub
#End Region

#Region "Wizard Common Code"
    'Codi comú a totes les wizards
    'es recomana no modificar
    'aquest codi fa crides a la regió Wizard Common Events,
    'on hi va el codi propietari

    Private Sub TabControl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPrevious.Click
        Dim oPageTarget As TabPage = TabControl1.TabPages(TabControl1.SelectedIndex - 1)
        Wizard_NavigateNext(TabControl1.SelectedTab, oPageTarget)
        TabControl1.SelectedTab = oPageTarget
        Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNext.Click
        Dim oPageTarget As TabPage = TabControl1.TabPages(TabControl1.SelectedIndex + 1)
        Wizard_NavigateNext(TabControl1.SelectedTab, oPageTarget)
        TabControl1.SelectedTab = oPageTarget
        Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEnd.Click
        Static BlDone As Boolean

        If BlDone Then
            Me.Close()
        Else
            BlDone = True
            Wizard_NavigateEnd()
            ButtonEnd.Text = "SORTIDA"
            ButtonPrevious.Enabled = False
        End If
    End Sub

    Private Sub EnableNavButtons()
        Dim Min As Integer = 0
        Dim Max As Integer = TabControl1.TabPages.Count - 1
        Dim Idx As Integer = TabControl1.SelectedIndex

        ButtonPrevious.Enabled = (Idx > Min)
        ButtonNext.Enabled = (Idx < Max)
        ButtonEnd.Enabled = (Idx = Max)
    End Sub

#End Region

    Private Sub CheckBoxNoDomiciliats_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNoDomiciliats.CheckedChanged
        If mallowevents Then
            LoadEfectes()
        End If
    End Sub

    Private Sub SEPA_Click(sender As Object, e As System.EventArgs) Handles _
        RadioButtonNorma58.Click, RadioButtonSepaB2B.Click

        Xl_Gir_SelEfts1.SepaMode = CurrentSepaMode()

    End Sub

    Private Function CurrentSepaMode() As Xl_Gir_SelEfts.Modes
        Dim oMode As Xl_Gir_SelEfts.Modes = Xl_Gir_SelEfts.Modes.SEPAB2B
        If RadioButtonNorma58.Checked Then oMode = Xl_Gir_SelEfts.Modes.Norma58
        If CheckBoxNoDomiciliats.Checked Then oMode = Xl_Gir_SelEfts.Modes.NoDomiciliats
        Return oMode
    End Function
End Class

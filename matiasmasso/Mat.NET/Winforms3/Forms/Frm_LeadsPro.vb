Public Class Frm_LeadsPro
    Private _value(10) As DTOLeadAreas
    Private _AllContactClasses As DTOContactClass.Collection
    Private _TabLoaded(10) As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Pro
        Consumers
    End Enum

    Private Async Sub Frm_Leads_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await LoadTab()
    End Sub

#Region "Pro"

    Private Async Function LoadTabPro() As Task
        Dim exs As New List(Of Exception)
        If Not _TabLoaded(Tabs.Pro) Then
            ProgressBarPro.Visible = True
            Dim oLabelInfo As New Label
            oLabelInfo.Text = "Un moment, si us plau. Estem carregant els contactes..."
            oLabelInfo.Dock = DockStyle.Fill
            oLabelInfo.Padding = New Padding(20, 50, 20, 50)
            oLabelInfo.BringToFront()
            SplitContainerPro.Panel1.Controls.Add(oLabelInfo)

            _value(Tabs.Pro) = Await FEB.LeadAreas.Pro(exs, GlobalVariables.Emp, Current.Session.Lang)
            Dim oAllContactClasses = Await FEB.ContactClasses.All(exs)
            If exs.Count = 0 Then
                SplitContainerPro.Panel1.Controls.Remove(oLabelInfo)
                Dim oFilterGuids = _value(Tabs.Pro).ContactClasses().Select(Function(x) x.Guid).ToList()
                _AllContactClasses = DTOContactClass.Collection.Factory(oAllContactClasses, oFilterGuids)
                Xl_ContactClassesCheckedTreePro.Load(_AllContactClasses)
                ProgressBarPro.Visible = False
                _TabLoaded(Tabs.Pro) = True
            Else
                SplitContainerPro.Panel1.Controls.Remove(oLabelInfo)
                ProgressBarPro.Visible = False
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Sub Xl_ContactClassesCheckedTreePro_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ContactClassesCheckedTreePro.AfterUpdate
        Xl_CheckedLeadAreasPro.Load(_value(Tabs.Pro), CheckedContactClasses())
        refrescaLeadsPro()
    End Sub

    Private Sub Xl_CheckedLeadAreasPro_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_CheckedLeadAreasPro.AfterUpdate
        refrescaLeadsPro()
    End Sub

    Private Sub refrescaLeadsPro()
        Dim oLeads = _value(Tabs.Pro).FilteredLeads(CheckedContactClasses(), CheckedProLocations())
        Dim oGuidNoms As List(Of DTOGuidNom) = oLeads.Select(Function(x) DTOGuidNom.Factory(x.Guid, x.Email)).ToList()
        Xl_CheckedGuidNomsPro.Load(oGuidNoms, oGuidNoms)
        LabelCountPro.Text = String.Format("Total leads: {0:#,0}", oLeads.Count)
    End Sub

    Private Function CheckedContactClasses() As DTOContactClass.Collection
        Return Xl_ContactClassesCheckedTreePro.CheckedValues()
    End Function

    Private Function CheckedProLocations() As List(Of DTOLeadAreas.Location)
        Return Xl_CheckedLeadAreasPro.CheckedLocations()
    End Function
#End Region

#Region "Consumer"
    Private Async Function LoadTabConsumer() As Task
        Dim exs As New List(Of Exception)
        If Not _TabLoaded(Tabs.Consumers) Then
            ProgressBarConsumer.Visible = True
            _value(Tabs.Consumers) = Await FEB.LeadAreas.Consumer(exs, GlobalVariables.Emp, Current.Session.Lang)
            If exs.Count = 0 Then
                Xl_CheckedLeadAreasConsumer.Load(_value(Tabs.Consumers))
                LabelCountConsumer.Text = String.Format("Total leads: {0:#,0}", 0)
                ProgressBarConsumer.Visible = False
                _TabLoaded(Tabs.Consumers) = True
            Else
                ProgressBarConsumer.Visible = False
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Sub Xl_CheckedLeadAreasConsumer_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_CheckedLeadAreasConsumer.AfterUpdate
        refrescaLeadsConsumer()
    End Sub

    Private Sub refrescaLeadsConsumer()
        Dim oLeads = _value(Tabs.Consumers).FilteredLeads(CheckedConsumerLocations())
        Dim oGuidNoms As List(Of DTOGuidNom) = oLeads.Select(Function(x) DTOGuidNom.Factory(x.Guid, x.Email)).ToList()
        Xl_CheckedGuidNomsConsumer.Load(oGuidNoms, oGuidNoms)
        LabelCountConsumer.Text = String.Format("Total leads: {0:#,0}", oLeads.Count)
    End Sub

    Private Function CheckedConsumerLocations() As List(Of DTOLeadAreas.Location)
        Return Xl_CheckedLeadAreasConsumer.CheckedLocations()
    End Function

#End Region



    Private Sub ExportarCsvToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarCsvToolStripMenuItem.Click
        Dim oCsv As DTOCsv = LeadsListControl(CurrentTab()).Csv()
        UIHelper.ShowCsv(oCsv)
    End Sub

    Private Sub ExportarExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet = LeadsListControl(CurrentTab()).Excel()
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function LeadsListControl(oTab As Tabs) As Xl_CheckedGuidNoms
        Dim retval As Xl_CheckedGuidNoms = Nothing
        Select Case CurrentTab()
            Case Tabs.Pro
                retval = Xl_CheckedGuidNomsPro
            Case Tabs.Consumers
                retval = Xl_CheckedGuidNomsConsumer
        End Select
        Return retval
    End Function

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Await LoadTab()
    End Sub

    Private Function CurrentTab() As Tabs
        Return TabControl1.SelectedIndex
    End Function

    Private Async Function LoadTab() As Task
        Dim exs As New List(Of Exception)
        If Not _TabLoaded(CurrentTab()) Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.Pro
                    Await LoadTabPro()
                Case Tabs.Consumers
                    Await LoadTabConsumer()
            End Select
        End If
    End Function

    Private Async Sub TornaACarregarElsProfessionalsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TornaACarregarElsProfessionalsToolStripMenuItem.Click
        _TabLoaded(Tabs.Pro) = False
        TabControl1.SelectedIndex = Tabs.Pro
        Await LoadTab()
    End Sub

    Private Async Sub TornaACarregarElsConsumidorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TornaACarregarElsConsumidorsToolStripMenuItem.Click
        _TabLoaded(Tabs.Consumers) = False
        TabControl1.SelectedIndex = Tabs.Consumers
        Await LoadTab()
    End Sub
End Class
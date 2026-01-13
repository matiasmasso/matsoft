Public Class Frm_MailingPros
    Private _AllLeads As List(Of DTOLeadChecked)
    Private _AllBrands As List(Of DTOProductBrand)

    Private _Channels As New List(Of DTODistributionChannel)
    Private _Brands As New List(Of DTOProductBrand)

    Public Sub New(Optional oChannels As List(Of DTODistributionChannel) = Nothing, Optional oBrands As List(Of DTOProductBrand) = Nothing)
        MyBase.New
        InitializeComponent()
        If oChannels IsNot Nothing Then _Channels = oChannels
        If oBrands IsNot Nothing Then _Brands = oBrands
    End Sub

    Private Async Sub Frm_MailingPros_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        _AllLeads = Await FEB.Mailing.XarxaDistribuidors(exs, DTO.GlobalVariables.Today().AddYears(-1))
        If exs.Count = 0 Then
            _AllBrands = Await FEB.ProductBrands.All(exs, Current.Session.Emp)
            If exs.Count = 0 Then
                Dim oAllChannels = Await FEB.DistributionChannels.Headers(GlobalVariables.Emp, Current.Session.Lang, exs)
                Cursor = Cursors.Default
                If exs.Count = 0 Then
                    Xl_DistributionChannels_Checklist1.Load(oAllChannels, _Channels)
                    LoadBrands()
                    RefrescaClients()
                    Await RefrescaReps()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function BrandLeads() As List(Of DTOLeadChecked)
        'Dim retval As New List(Of DTOLeadChecked)
        Dim oBrands As List(Of DTOProductBrand) = CurrentBrands()
        Dim oChannels As List(Of DTODistributionChannel) = CurrentChannels()
        Dim oLeads As List(Of DTOLeadChecked) = DTOMailing.XarxaDistribuidors(_AllLeads, oChannels, oBrands)
        'For Each item As DTOLeadChecked In _AllLeads
        ' Dim BlChecked As Boolean = oLeads.Exists(Function(x) x.EmailAddress = item.EmailAddress)
        ' retval.Add(New DTOLeadChecked With {.EmailAddress = item.EmailAddress, .Checked = BlChecked})
        ' Next
        Return oLeads
    End Function

    Private Async Function BrandReps(exs As List(Of Exception)) As Task(Of List(Of DTOLeadChecked))
        Dim oBrands As List(Of DTOProductBrand) = CurrentBrands()
        Dim oChannels As List(Of DTODistributionChannel) = CurrentChannels()
        Dim retval = Await FEB.Mailing.Reps(exs, oChannels, oBrands)
        Return retval
    End Function

    Private Async Function refresca() As Task
        RefrescaClients()
        Await RefrescaReps()
    End Function

    Private Sub RefrescaClients()
        Dim oLeads As List(Of DTOLeadChecked) = BrandLeads()
        Dim iCount As Integer = oLeads.Where(Function(x) x.Checked = True).Count
        Xl_leadsCheckedClients.Load(oLeads)
        RefrescaClientsCount()
        LabelGlobalCount.Text = String.Format("Total {0:#,###} destinataris", iCount)
    End Sub

    Private Async Function RefrescaReps() As Task
        Dim exs As New List(Of Exception)
        Dim oLeads = Await BrandReps(exs)
        If exs.Count = 0 Then
            Dim iCount As Integer = oLeads.Where(Function(x) x.Checked = True).Count
            Xl_leadsCheckedReps.Load(oLeads)
            LabelGlobalCount.Text = String.Format("Total {0:#,###} representants", iCount)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Brands_CheckList1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Brands_CheckList1.AfterUpdate
        RefrescaClients()
        Await RefrescaReps()
    End Sub

    Private Function CurrentBrands() As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = Nothing
        If CheckBoxBrands.Checked Then
            retval = Xl_Brands_CheckList1.selectedBrands
        Else
            retval = _AllBrands
        End If
        Return retval
    End Function

    Private Function CurrentLeads() As List(Of DTOLeadChecked)
        Dim retval As List(Of DTOLeadChecked) = Xl_leadsCheckedClients.CheckedItems
        Return retval
    End Function

    Private Function CurrentChannels() As List(Of DTODistributionChannel)
        Dim retval As List(Of DTODistributionChannel) = Xl_DistributionChannels_Checklist1.SelectedValues
        Return retval
    End Function

    Private Async Sub Xl_DistributionChannels_Checklist1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_DistributionChannels_Checklist1.AfterUpdate
        Await refresca()
    End Sub

    Private Async Sub CheckBoxBrands_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxBrands.CheckedChanged
        If CheckBoxBrands.Checked Then
            With Xl_Brands_CheckList1
                .Enabled = True
            End With
        Else
            With Xl_Brands_CheckList1
                .Enabled = False
                .ClearChecks()
            End With
        End If
        Await refresca()
    End Sub

    Private Sub LoadBrands()
        With Xl_Brands_CheckList1
            .Load(_AllBrands, _Brands)
            .Enabled = False
        End With
    End Sub

    Private Sub ExportarCsvToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarCsvToolStripMenuItem.Click
        Dim oCsv As New DTOCsv("circular.csv")
        Dim oLeads As List(Of DTOLeadChecked) = CurrentLeads()

        For Each item As DTOLeadChecked In oLeads
            Dim oRow = oCsv.AddRow()
            oRow.addcell(item.Guid.ToString())
            oRow.addcell(item.EmailAddress)
            'If item.EmailAddress.EndsWith(".pt") Then Stop
            oRow.addcell(item.Lang.Tag)
        Next

        UIHelper.SaveCsvDialog(oCsv, "desar destinataris circular")
    End Sub

    Private Sub Xl_TextboxSearchClients_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearchClients.AfterUpdate
        Xl_leadsCheckedClients.Filter = e.Argument
        RefrescaClientsCount()
    End Sub

    Private Sub RefrescaClientsCount()
        Dim iCount As Integer = Xl_leadsCheckedClients.CheckedItems.Count
        Dim sCount As String = ""
        Select Case iCount
            Case 0
                sCount = "sense destinataris"
            Case 1
                sCount = String.Format("1 sol destinatari", iCount)
            Case Else
                sCount = String.Format("{0:#,###} destinataris", iCount)
        End Select
        LabelClientsCount.Text = sCount
        RefrescaGlobalCount()
    End Sub

    Private Sub RefrescaGlobalCount()
        Dim sCount As String = ""
        Dim iCount As Integer = Xl_leadsCheckedClients.CheckedItems.Count
        iCount += Xl_leadsCheckedReps.CheckedItems.Count
        Select Case iCount
            Case 0
                sCount = "sense destinataris"
            Case 1
                sCount = String.Format("total 1 destinatari", iCount)
            Case Else
                sCount = String.Format("total {0:#,###} destinataris", iCount)
        End Select
        LabelGlobalCount.Text = sCount
    End Sub

End Class
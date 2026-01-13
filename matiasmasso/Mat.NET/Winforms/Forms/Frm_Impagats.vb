

Public Class Frm_Impagats
    Private _Impagats As List(Of DTOImpagat)
    Private _Mems As List(Of DTOMem)
    Private _AllowEvents As Boolean

    Private Enum Cols2
        Guid
        vto
        Nominal
        Despeses
        txt
    End Enum



    Private Async Sub Frm_Impagats_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Impagats = Await FEB2.Impagats.All(exs, Current.Session.User)
        _Mems = Await FEB2.Mems.Impagats(Current.Session.Emp, exs)

        If exs.Count = 0 Then
            LoadSummary()
            LoadDetail()
            LoadMems()
            LoadStatus()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadSummary()
        Xl_ImpagatsSummary1.Load(_Impagats, _Mems)
    End Sub

    Private Sub LoadDetail()
        Dim oContact As DTOContact = CurrentContact()
        Dim oImpagats As List(Of DTOImpagat) = _Impagats.FindAll(Function(x) x.Csb.Contact.Equals(oContact))
        Xl_ImpagatsDetail1.Load(oImpagats)
    End Sub

    Private Sub LoadMems()
        Dim oContact As DTOContact = CurrentContact()

        If oContact IsNot Nothing Then
            Dim oMems As List(Of DTOMem) = _Mems.FindAll(Function(x) x.Contact.Equals(oContact)).ToList
            Xl_Mems1.Load(oMems)
        End If
    End Sub

    Private Sub LoadStatus()
        Dim efts As Integer = _Impagats.Count
        Dim clis As Integer = _Impagats.Select(Function(x) x.Csb.Contact.Guid).Distinct.Count
        Dim eur As Decimal = _Impagats.Sum(Function(x) x.Csb.Amt.Eur)
        ToolStripStatusLabelTotals.Text = String.Format("Total {0:0} impagats de {1:0} clients per {2:#,##0.00 €}", efts, clis, eur)
    End Sub


    Private Function CurrentImpagat() As DTOImpagat
        Dim RetVal As DTOImpagat = Xl_ImpagatsDetail1.Value
        Return RetVal
    End Function

    Private Function CurrentContact() As DTOContact
        Dim RetVal As DTOContact = Xl_ImpagatsSummary1.Value
        Return RetVal
    End Function


    Private Sub Xl_ImpagatsSummary1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ImpagatsSummary1.ValueChanged
        If _AllowEvents Then
            Dim oContact As DTOContact = CurrentContact()
            Dim oImpagats As List(Of DTOImpagat) = _Impagats.Where(Function(x) x.Csb.Contact.Equals(oContact)).ToList
            Xl_ImpagatsDetail1.Load(oImpagats)
            LoadMems()
        End If
    End Sub


    Private Sub Xl_Mems1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Mems1.RequestToAddNew
        Dim oMem As New DTOMem()
        With oMem
            .Contact = Xl_ImpagatsSummary1.Value.ToGuidNom()
            .Fch = Now
            .Cod = DTOMem.Cods.Impagos
            .UsrLog = DTOUsrLog2.Factory(Current.Session.User)
        End With

        Dim oFrm As New Frm_Mem(oMem)
        AddHandler oFrm.AfterUpdate, AddressOf LoadMems
        oFrm.Show()
    End Sub


    Private Sub Xl_Mems1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Mems1.RequestToRefresh
        LoadMems()
    End Sub

    Private Sub Xl_ImpagatsDetail1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ImpagatsDetail1.RequestToRefresh
        LoadSummary()
        LoadDetail()
    End Sub

    Private Sub Xl_ImpagatsDetail1_RequestToRefreshMasterGrid(sender As Object, e As MatEventArgs) Handles Xl_ImpagatsDetail1.RequestToRefreshMasterGrid
        Xl_ImpagatsSummary1.RefreshFromDetail(Me, e)
    End Sub
End Class
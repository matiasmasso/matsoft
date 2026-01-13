Public Class Frm_Banc_SepaMes
    Private _Banc As DTOBanc
    Private _Values As List(Of DTOSepaMe)
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse
    Private _AllowEvents As Boolean

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oBanc As DTOBanc = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Banc = oBanc
    End Sub

    Private Async Sub Frm_SepaMes_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Banc Is Nothing Then
            Me.Text = "Sepas signats"
            Await refresca()
        Else
            If FEB.Contact.Load(_Banc, exs) Then
                Me.Text = "Sepas signats a " & _Banc.NomComercialOrDefault
                Await refresca()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
        _AllowEvents = True
    End Sub

    Private Sub Xl_SepaMes1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_SepaMes1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_SepaMes1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_SepaMes1.RequestToAddNew
        Do_AddNew()
    End Sub

    Private Async Sub Xl_SepaMes1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SepaMes1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Values = Await FEB.SepaMes.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            LoadBancs()
            Xl_SepaMes1.Load(FilteredValues())
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function FilteredValues() As List(Of DTOSepaMe)
        Dim retval As List(Of DTOSepaMe) = _Values
        Dim searchKey = Xl_TextboxSearch1.Value
        If Not String.IsNullOrEmpty(searchKey) Then
            retval = _Values.Where(Function(x) Xl_TextboxSearch1.IsContainedIn(x.Lliurador.FullNom)).ToList()
        End If

        Dim oCurrentBanc = CurrentBanc()
        If oCurrentBanc IsNot Nothing Then
            retval = retval.Where(Function(x) x.Banc.Guid.Equals(oCurrentBanc.Guid)).ToList()
        End If
        Return retval
    End Function

    Private Async Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        Await refresca()
    End Sub

    Private Sub AfegirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AfegirToolStripMenuItem.Click
        Do_AddNew()
    End Sub

    Private Sub Do_AddNew()
        Dim oSepaMe = DTOSepaMe.Factory(_Banc, Current.Session.User)
        Dim oFrm As New Frm_SepaMe(oSepaMe)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_SepaMes1.Load(FilteredValues())
    End Sub

    Private Sub LoadBancs()
        Dim oBancs = _Values.GroupBy(Function(x) x.Banc.Guid).Select(Function(y) y.First).Select(Function(z) z.Banc).OrderBy(Function(s) s.NomComercialOrDefault).ToList()
        Dim oItems As New List(Of DTOGuidNom)
        oItems.Add(New DTOGuidNom(Guid.Empty, "(tots els bancs)"))
        oItems.AddRange(oBancs.Select(Function(x) New DTOGuidNom(x.Guid, x.Abr)))

        ComboBoxBanc.DataSource = oItems
        ComboBoxBanc.DisplayMember = "Nom"
        If _Banc Is Nothing Then
            ComboBoxBanc.SelectedIndex = 0
        Else
            ComboBoxBanc.SelectedIndex = oItems.IndexOf(oItems.FirstOrDefault(Function(x) x.Guid.Equals(_Banc.Guid)))
        End If
    End Sub

    Private Function CurrentBanc() As DTOGuidNom
        Dim retval As DTOGuidNom = Nothing
        If ComboBoxBanc.SelectedIndex > 0 Then
            retval = ComboBoxBanc.SelectedItem
        End If
        Return retval
    End Function

    Private Sub ComboBoxBanc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxBanc.SelectedIndexChanged
        If _AllowEvents Then
            Xl_SepaMes1.Load(FilteredValues())
            Me.Text = "Sepas signats"
        End If
    End Sub
End Class
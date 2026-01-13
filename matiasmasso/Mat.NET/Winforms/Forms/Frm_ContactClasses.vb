Public Class Frm_ContactClasses

    Private _DefaultValue As DTOContactClass
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event ItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOContactClass = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Sub Frm_ContactClasses_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_ContactClasses1_onItemSelected(sender As Object, e As MatEventArgs)
        RaiseEvent ItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim oContactClasses = Await FEB2.ContactClasses.All(exs)
        If exs.Count = 0 Then
            Xl_ContactClassesTree1.Load(oContactClasses, Current.Session.Lang, _DefaultValue)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ContactClassesTree1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ContactClassesTree1.RequestToAddNew
        Dim exs As New List(Of Exception)
        Dim oContactClass As New DTOContactClass
        oContactClass.DistributionChannel = DTODistributionChannel.Wellknown(DTODistributionChannel.Wellknowns.Diversos)
        If FEB2.DistributionChannel.Load(oContactClass.DistributionChannel, exs) Then
            Dim oFrm As New Frm_ContactClass(oContactClass)
            AddHandler oFrm.AfterUpdate, AddressOf refresca
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ContactClassesTree1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ContactClassesTree1.RequestToRefresh
        refresca()
    End Sub

    Private Sub Xl_ContactClassesTree1_ItemSelected(sender As Object, e As MatEventArgs) Handles Xl_ContactClassesTree1.ItemSelected
        If _SelectionMode = DTO.Defaults.SelectionModes.Selection Then
            RaiseEvent ItemSelected(Me, e)
            Me.Close()
        End If
    End Sub
End Class
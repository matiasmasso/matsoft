Public Class Frm_StaffScheds

    Private _Emp As DTOEmp
    Private _Staff As DTOStaff
    Private _Values As List(Of DTOStaffSched)
    Private _Holidays As List(Of DTOStaffHoliday)

    Public Sub New(oEmp As DTOEmp)
        MyBase.New
        InitializeComponent()
        _Emp = oEmp
    End Sub

    Public Sub New(oStaff As DTOStaff)
        MyBase.New
        InitializeComponent()
        _Emp = GlobalVariables.Emp
        _Staff = oStaff
    End Sub

    Private Sub Frm_StaffSched_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBoxTitular.Text = TitularNom()
        reloadScheds()
        reloadHolidays()
    End Sub


    Private Async Sub Xl_StaffScheds1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_StaffScheds1.RequestToAddNew
        Dim value = DTOStaffSched.Factory(_Emp, _Staff)
        If _Staff IsNot Nothing Then
            Dim exs As New List(Of Exception)
            Dim oDefaultValue = Await FEB.StaffSched.Vigent(_Emp, exs)
            If exs.Count = 0 Then
                value.Items = oDefaultValue.Items
            Else
                UIHelper.WarnError(exs)
            End If
        End If
        Dim oFrm As New Frm_StaffSched(value)
        AddHandler oFrm.AfterUpdate, AddressOf reloadScheds
        oFrm.Show()
    End Sub

    Private Sub Xl_StaffHolidays1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_StaffHolidays1.RequestToAddNew
        Dim value = DTOStaffHoliday.Factory(_Emp, _Staff)
        Dim oFrm As New Frm_StaffHoliday(value)
        AddHandler oFrm.AfterUpdate, AddressOf reloadHolidays
        oFrm.Show()
    End Sub

    Private Function Titular() As Object
        If _Staff Is Nothing Then
            Return _Emp
        Else
            Return _Staff
        End If
    End Function

    Private Function TitularNom() As String
        If _Staff Is Nothing Then
            Return _Emp.Nom
        Else
            Return _Staff.Abr
        End If
    End Function

    Private Async Sub reloadScheds()
        Dim exs As New List(Of Exception)
        _Values = Await FEB.StaffScheds.All(Titular, exs)
        If exs.Count = 0 Then
            Xl_StaffScheds1.Load(_Values)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub reloadHolidays()
        Dim exs As New List(Of Exception)
        _Holidays = Await FEB.StaffHolidays.All(_Emp, _Staff, exs)
        If exs.Count = 0 Then
            Xl_StaffHolidays1.Load(_Holidays)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_StaffHolidays1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_StaffHolidays1.RequestToRefresh
        reloadHolidays()
    End Sub

    Private Sub Xl_StaffScheds1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_StaffScheds1.RequestToRefresh
        reloadScheds()
    End Sub
End Class
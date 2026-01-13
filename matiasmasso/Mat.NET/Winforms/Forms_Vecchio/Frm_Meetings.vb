Public Class Frm_Meetings
    Private _Contact As DTOContact

    Public Sub New(Optional oContact As DTOContact = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
    End Sub

    Private Sub Frm_Meetings_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        Dim oMeetings As List(Of DTOMeeting) = BLL_Meetings.All(_Contact)
        Xl_Meetings1.Load(oMeetings)
    End Sub

    Private Sub Xl_Meetings1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Meetings1.RequestToAddNew
        Dim oMeeting As DTOMeeting = BLL_Meeting.NewMeeting
        Dim oFrm As New Frm_Meeting(oMeeting)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Meetings1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Meetings1.RequestToRefresh
        refresca()
    End Sub
End Class
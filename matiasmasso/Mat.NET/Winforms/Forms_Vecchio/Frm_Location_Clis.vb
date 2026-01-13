
Public Class Frm_Location_Clis

    Private _Location As DTOLocation

    Public Sub New(oLocation As DTOLocation)
        MyBase.New()
        Me.InitializeComponent()
        _Location = oLocation
    End Sub

    Private Sub Frm_Location_Clis_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "Clients a " & _Location.FullNom(Current.Session.User.Lang)

        Dim oQuery = DTOCustomerRanking.Factory(Current.Session.User, oArea:=_Location)
        Dim exs As New List(Of Exception)
        If FEB2.CustomerRanking.Load(oQuery, exs) Then
            Xl_LocationClis1.Load(oQuery)
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub
End Class
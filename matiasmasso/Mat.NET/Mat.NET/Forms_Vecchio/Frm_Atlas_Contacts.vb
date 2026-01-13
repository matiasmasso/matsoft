Public Class Frm_Atlas_Contacts

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oAtlas As DTOAtlas, Optional oSelectedArea As DTOAreaOld = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        Xl_Atlas_Contacts1.Load(oAtlas, oSelectedArea)
    End Sub

    Private Sub Xl_Atlas_Contacts1_OnItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Atlas_Contacts1.OnItemSelected
        RaiseEvent OnItemSelected(Me, e)
        Me.Close()
    End Sub
End Class
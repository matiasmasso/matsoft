

Public Class Frm_ArtCustRef
    Private mArtCustRef As ArtCustRef = Nothing
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oArtCustRef As ArtCustRef)
        MyBase.new()
        Me.InitializeComponent()
        mArtCustRef = oArtCustRef
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mArtCustRef
            If .Client IsNot Nothing Then
                Xl_Contact1.Contact = .Client
            End If

            If .Art IsNot Nothing Then
                Xl_Art1.Art = .Art
                PictureBox1.Image = .Art.Image
            End If

            TextBoxRef.Text = .Ref

            If .Exists Then
                ButtonDel.Enabled = True
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         Xl_Art1.AfterUpdate, _
          TextBoxRef.TextChanged

        If mAllowEvents Then
            If Xl_Contact1.Contact IsNot Nothing And Xl_Art1.Art IsNot Nothing And TextBoxRef.Text > "" Then
                ButtonOk.Enabled = True
            Else
                ButtonOk.Enabled = False
            End If
        End If
    End Sub

    Private Sub Xl_Contact1_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_Contact1.AfterUpdate
        If mAllowEvents Then
            Dim oContact As Contact = Xl_Contact1.Contact
            If oContact IsNot Nothing Then
                Dim oClient As New Client(oContact.Guid)
                Dim oCcx As Client = oClient.CcxOrMe
                If Not oContact.Equals(oCcx) Then
                    Xl_Contact1.Contact = oCcx
                End If

                If Xl_Art1.Art IsNot Nothing And TextBoxRef.Text > "" Then
                    ButtonOk.Enabled = True
                Else
                    ButtonOk.Enabled = False
                End If
            Else
                ButtonOk.Enabled = False
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mArtCustRef
            .Client = New Client(Xl_Contact1.Contact.Guid)
            .Art = Xl_Art1.Art
            .Ref = TextBoxRef.Text
            .Update()
            RaiseEvent AfterUpdate(mArtCustRef, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        mArtCustRef.Delete()
        Me.Close()
    End Sub


End Class
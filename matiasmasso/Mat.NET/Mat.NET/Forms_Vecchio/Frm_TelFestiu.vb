

Public Class Frm_TelFestiu
    Private mFestiu As TelFestiu
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oFestiu As TelFestiu)
        MyBase.new()
        Me.InitializeComponent()
        mFestiu = oFestiu
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mFestiu
            TextBoxNom.Text = .Nom
            If .Exists Then
                DateTimePickerFrom.Value = IIf(.FchFrom = Date.MinValue, Today, .FchFrom)
                DateTimePickerTo.Value = IIf(.FchTo = Date.MinValue, Today, .FchTo)
                CheckBoxRecursiu.Checked = .Recursiu
                Xl_Lookup_TelMissatges1.Missatge = .Missatge
                ButtonDel.Enabled = .AllowDelete
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged, _
         DateTimePickerFrom.ValueChanged, _
          DateTimePickerTo.ValueChanged, _
           CheckBoxRecursiu.CheckedChanged, _
            Xl_Lookup_TelMissatges1.AfterUpdate

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mFestiu
            .Nom = TextBoxNom.Text
            .FchFrom = DateTimePickerFrom.Value
            .FchTo = DateTimePickerTo.Value
            .Recursiu = CheckBoxRecursiu.Checked
            .Missatge = Xl_Lookup_TelMissatges1.Missatge
            .Update()
            RaiseEvent AfterUpdate(mFestiu, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mFestiu.AllowDelete Then
            mFestiu.Delete()
            RaiseEvent AfterUpdate(sender, EventArgs.Empty)
            Me.Close()
        End If
    End Sub



    Private Sub ButtonPlayWav_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPlayWav.Click
        Dim oPreviousCursor As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        'Dim oVoice As New SpeechLib.SpVoice
        'Dim cpFileStream As New SpeechLib.SpFileStream


        'Dim x As New SpeechLib.SpVoice
        'Dim arrVoices As SpeechLib.ISpeechObjectTokens = x.GetVoices
 

        'oVoice.Voice = arrVoices.Item(1)
        'oVoice.Volume = 
        'oVoice.Speak(mFestiu.Missatge.TxtEsp, SpeechLib.SpeechVoiceSpeakFlags.SVSFDefault)
        'oVoice = Nothing
        Me.Cursor = oPreviousCursor
    End Sub
End Class
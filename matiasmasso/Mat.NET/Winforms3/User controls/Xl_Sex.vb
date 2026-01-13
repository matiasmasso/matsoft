Public Class Xl_Sex
    Private mSex As DTOUser.Sexs = DTOUser.Sexs.NotSet
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As EventArgs)

    Public Property Sex() As DTOUser.Sexs
        Get
            Return mSex
        End Get
        Set(ByVal value As DTOUser.Sexs)
            mSex = value
            refresca()
            mAllowEvents = True
        End Set
    End Property

    Private Sub refresca()
        mAllowEvents = False
        ToolStripMenuItemMale.Checked = (mSex = DTOUser.Sexs.Male)
        ToolStripMenuItemFemale.Checked = (mSex = DTOUser.Sexs.Female)
        ToolStripMenuItemNotSet.Checked = (mSex = DTOUser.Sexs.NotSet)

        Select Case mSex
            Case DTOUser.Sexs.Male
                PictureBox1.Image = My.Resources.SexMale
            Case DTOUser.Sexs.Female
                PictureBox1.Image = My.Resources.SexFemale
            Case Else
                PictureBox1.Image = My.Resources.SexPending
        End Select
        mAllowEvents = True
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        Select Case mSex
            Case DTOUser.Sexs.Male
                mSex = DTOUser.Sexs.Female
            Case DTOUser.Sexs.Female
                mSex = DTOUser.Sexs.NotSet
            Case Else
                mSex = DTOUser.Sexs.Male
        End Select
        refresca()
        RaiseEvent AfterUpdate(mSex, EventArgs.Empty)
    End Sub

    Private Sub ToolStripMenuItem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    ToolStripMenuItemMale.CheckedChanged,
     ToolStripMenuItemFemale.CheckedChanged,
      ToolStripMenuItemNotSet.CheckedChanged

        If mAllowEvents Then
            If ToolStripMenuItemMale.Checked Then
                mSex = DTOUser.Sexs.Male
            ElseIf ToolStripMenuItemFemale.Checked Then
                mSex = DTOUser.Sexs.Female
            Else
                mSex = DTOUser.Sexs.NotSet
            End If
            refresca()
            RaiseEvent AfterUpdate(mSex, EventArgs.Empty)
        End If
    End Sub
End Class

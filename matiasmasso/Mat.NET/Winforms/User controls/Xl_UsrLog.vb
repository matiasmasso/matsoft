Public Class Xl_UsrLog
    Inherits TextBox

    Public Shadows Sub Load(oUsrLog As DTOUsrLog2)
        Me.Enabled = False
        Me.ReadOnly = True
        MyBase.Text = oUsrLog.text
        MyBase.BackColor = SystemColors.Control
    End Sub

    Public Shadows Sub Load(oUsrLog As DTOUsrLog)
        Me.Enabled = False
        Me.ReadOnly = True

        Dim sb As New System.Text.StringBuilder

        If oUsrLog IsNot Nothing Then
            With oUsrLog
                If .usrCreated Is Nothing Then
                    If .fchCreated = Nothing Then
                        sb.Append("(nou registre creat per " & DTOUser.NicknameOrElse(Current.Session.User) & ")")
                    End If
                Else
                    sb.Append(String.Format("Creat per {0}", DTOUser.NicknameOrElse(.usrCreated)))
                    If .fchCreated <> Nothing Then
                        sb.Append(String.Format(" el {0:dd/MM/yy} a les {0:HH:mm}", .fchCreated))
                        If .usrLastEdited IsNot Nothing Then
                            Dim gap As TimeSpan = .fchLastEdited - .fchCreated
                            If (gap.Minutes > 15) Or .usrCreated.unEquals(.usrLastEdited) Then
                                If .usrCreated.Equals(.usrLastEdited) Then
                                    sb.Append(String.Format(", modificat per el mateix usuari el {0:dd/MM/yy} a les {0:HH:mm}", .fchLastEdited))
                                Else
                                    sb.Append(String.Format(", modificat per {0} el {1:dd/MM/yy} a les {1:HH:mm}", DTOUser.NicknameOrElse(.usrLastEdited), .fchLastEdited))
                                End If
                            End If
                        End If
                    End If
                End If
            End With
        End If
        MyBase.Text = sb.ToString
        MyBase.BackColor = SystemColors.Control
    End Sub
End Class

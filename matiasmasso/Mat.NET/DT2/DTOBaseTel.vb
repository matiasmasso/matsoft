Public Class DTOBaseTel
    Inherits DTOBaseGuid

    Property value As String
    Property obs As String

    Property objCod As ObjCods

    Public Enum ObjCods
        Tel
        User
    End Enum

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Sub New() 'Constructor sense parametres per serialitzar-lo al pujar les dades via Ajax per exemple de Quiz
        MyBase.New()
    End Sub

    Shared Function IsPhoneNumber(src As String) As String
        Dim retval As Boolean = True
        Dim allowedChars As String = "+()-.0123456789 "
        For i = 0 To src.Length - 1
            Dim StringToCheck As String = src.Substring(i, 1)
            If Not allowedChars.Contains(StringToCheck) Then
                Return False
                Exit For
            End If
        Next
        Return retval
    End Function

    Shared Function Formatted(src As String, Optional oCountry As DTOCountry = Nothing, Optional IncludeSpanishPrefix As Boolean = False) As String
        Dim retval As String = ""
        If src > "" Then
            Dim onlyDigits As New String(src.Where(Function(x) Char.IsDigit(x)).ToArray())
            retval = TextHelper.InsertStringRepeatedly(onlyDigits, ".", 3)
            If oCountry IsNot Nothing Then
                If IncludeSpanishPrefix Or oCountry.ISO <> "ES" Then
                    retval = String.Format("(+{0}) {1}", oCountry.PrefixeTelefonic, retval)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function Formatted(src As DTOBaseTel, Optional ShowCountryPrefix As Boolean = True) As String
        Dim retval As String = ""
        If TypeOf src Is DTOContactTel Then
            Dim oContactTel As DTOContactTel = src
            If ShowCountryPrefix Then
                retval = Formatted(oContactTel.value, oContactTel.Country)
            Else
                retval = Formatted(oContactTel.value)
            End If
        ElseIf TypeOf src Is DTOUser Then
            Dim oUser As DTOUser = src
            retval = oUser.EmailAddress
        End If
        Return retval
    End Function

    Shared Function GetObs(src As DTOBaseTel) As String
        Dim retval As String = ""
        If TypeOf src Is DTOContactTel Then
            Dim oContactTel As DTOContactTel = src
            retval = oContactTel.Obs
        ElseIf TypeOf src Is DTOUser Then
            Dim oEmail As DTOUser = src
            retval = oEmail.Obs
        End If
        Return retval
    End Function
End Class

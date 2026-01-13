Public Class DTOContactTel
    Inherits DTOBaseTel

    Property cod As Cods
    Property ord As Integer
    Property country As DTOCountry
    Property privat As Boolean

    Public Enum IncludePrefix
        None
        IfNotCurrent
        Always
    End Enum


    Public Enum Cods
        NotSet
        tel
        fax
        movil
        email
    End Enum

    Public Sub New()
        MyBase.New()
        MyBase.objCod = DTOBaseTel.ObjCods.Tel
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        MyBase.objCod = DTOBaseTel.ObjCods.Tel
    End Sub

    Shared Function Factory(oContact As DTOContact, Optional oCod As DTOContactTel.Cods = DTOContactTel.Cods.tel, Optional value As String = "") As DTOContactTel
        Dim retval As New DTOContactTel
        With retval
            .cod = oCod
            .country = DTOAddress.Country(oContact.address)
            .value = TextHelper.LeaveJustNumbericDigits(value)
        End With
        Return retval
    End Function

    Shared Function CleanValue(oContactTel As DTOContactTel) As String
        Dim retval As String = ""
        If oContactTel IsNot Nothing Then
            retval = TextHelper.LeaveJustNumbericDigits(oContactTel.value)
        End If
        Return retval
    End Function

    Shared Shadows Function Formatted(value As DTOContactTel) As String
        Dim retval As String = ""
        If value IsNot Nothing Then
            retval = value.value
            If retval.Length > 6 Then
                retval = retval.Insert(6, ".")
                retval = retval.Insert(3, ".")
            End If
            If value.country IsNot Nothing Then
                If Not DTOArea.IsEsp(value.country) Then
                    If value.country.prefixeTelefonic > "" Then
                        retval = "(+" & value.country.prefixeTelefonic & ") " & retval
                    End If
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function HtmlLink(value As DTOContactTel) As String
        Dim retval As String = value.value
        If value.country IsNot Nothing Then
            If value.country.prefixeTelefonic > "" Then
                retval = String.Format("+{0}{1}", value.country.prefixeTelefonic, value.value)
            End If
        End If
        Return retval
    End Function

    Shared Function Html5Formatted(oContactTel As DTOContactTel) As String
        Dim retval As String = ""
        If oContactTel IsNot Nothing Then
            If oContactTel.value > "" Then
                Dim sTelnum As String = System.Text.RegularExpressions.Regex.Replace(oContactTel.value, "[^0-9]", "")
                If oContactTel.country IsNot Nothing AndAlso oContactTel.country.prefixeTelefonic > "" Then
                    retval = String.Format("tel:+{0}{1}", oContactTel.country.prefixeTelefonic, sTelnum)
                Else
                    retval = String.Format("tel:{0}", sTelnum)
                End If
            End If
        End If
        Return retval
    End Function
End Class

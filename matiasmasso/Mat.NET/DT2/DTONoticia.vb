Public Class DTONoticia
    Inherits DTONoticiaBase

    Property product As DTOProduct
    Property priority As PriorityLevels

    Public Sub New()
        MyBase.New(Srcs.News)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Enum PriorityLevels
        Low
        High
    End Enum

    Public Enum wellknowns
        NotSet
        iMat2
    End Enum

    Shared Function wellknown(id As DTONoticia.wellknowns) As DTONoticia
        Dim retval As DTONoticia = Nothing
        Select Case id
            Case DTONoticia.wellknowns.iMat2
                retval = New DTONoticia(New Guid("12C5299A-573C-4A90-860F-3CDA98A713AD"))
        End Select
        Return retval
    End Function


    Shared Function ExcerptOrCroppedText(oNoticia As DTONoticia) As DTOLangText
        Dim retval As New DTOLangText
        If oNoticia.Excerpt IsNot Nothing Then
            If Not oNoticia.Excerpt.IsEmpty Then
                retval = oNoticia.Excerpt
            End If
        End If

        If retval.IsEmpty AndAlso oNoticia.Text IsNot Nothing Then

            Dim sText As String = oNoticia.Text.Esp
            If sText > "" Then

                Dim iMore As Integer = sText.IndexOf("<more/>")
                If iMore < 0 Then iMore = 255
                retval.Esp = sText.Substring(0, iMore)

                sText = oNoticia.Text.Cat
                If sText.Length > 0 Then
                    iMore = sText.IndexOf("<more/>")
                    If iMore < 0 Then iMore = 255
                    retval.Cat = sText.Substring(0, iMore)
                End If

                sText = oNoticia.Text.Eng
                If sText.Length > 0 Then
                    iMore = sText.IndexOf("<more/>")
                    If iMore < 0 Then iMore = 255
                    retval.Eng = sText.Substring(0, iMore)
                End If

                sText = oNoticia.Text.Por
                If sText.Length > 0 Then
                    iMore = sText.IndexOf("<more/>")
                    If iMore < 0 Then iMore = 255
                    retval.Por = sText.Substring(0, iMore)
                End If
            End If
        End If

        Return retval
    End Function

    Shared Function Filtered(oNoticia As DTONoticia, oUser As DTOUser) As DTOLangText
        Dim retval As DTOLangText = Nothing
        If oUser Is Nothing Then
            retval = DTOLangText.Replace(oNoticia.Text, "@UserGuid", "")
        Else
            retval = DTOLangText.Replace(oNoticia.Text, "@UserGuid", oUser.Guid.ToString())
        End If
        Return retval
    End Function
End Class

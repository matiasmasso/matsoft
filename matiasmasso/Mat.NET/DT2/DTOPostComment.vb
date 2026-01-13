Public Class DTOPostComment
    Inherits DTOBaseGuid
    Property Parent As Guid
    Property ParentSource As ParentSources
    Property User As DTOUser
    Property Lang As DTOLang
    Property Fch As Date
    Property Text As String
    Property FchApproved As Date
    Property FchDeleted As Date
    Property Answering As DTOPostComment
    Property Answers As New List(Of DTOPostComment)

    Private _ParentTitle As DTOLangText

    Property ErrorCode As ErrorCodes

    Public Enum StatusEnum
        NotSet
        Pendent
        Aprobat
        Eliminat
    End Enum

    Public Enum ParentSources
        NotSet
        Noticia
        Blog
    End Enum

    Public Enum ErrorCodes
        None
        NotLogged
    End Enum

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function NewFromParent(oParent As Guid) As DTOPostComment
        Dim retval As New DTOPostComment()
        retval.Parent = oParent
        Return retval
    End Function

    Shared Function NewFromParentSource(oGuid As Guid) As DTOPostComment
        Dim retval As New DTOPostComment()
        retval.Parent = oGuid
        retval.Fch = Now
        Return retval
    End Function

    Shared Function FromQuestion(oAnsweringComment As DTOPostComment, sText As String) As DTOPostComment
        Dim retval As New DTOPostComment
        With retval
            .Answering = oAnsweringComment
            .Parent = oAnsweringComment.Parent
            .ParentSource = oAnsweringComment.ParentSource
            .User = DTOUser.wellknown(DTOUser.wellknowns.matias)
            .FchApproved = Now
            .Fch = Now
            .Text = sText
        End With
        Return retval
    End Function

    Public ReadOnly Property Status() As StatusEnum
        Get
            Dim retval As StatusEnum = StatusEnum.NotSet
            If _FchDeleted = Nothing Then
                If _FchApproved = Nothing Then
                    retval = StatusEnum.Pendent
                Else
                    retval = StatusEnum.Aprobat
                End If
            Else
                retval = StatusEnum.Eliminat
            End If
            Return retval
        End Get
    End Property

    Public ReadOnly Property NickName As String
        Get
            Dim retval As String = ""
            If _User IsNot Nothing Then
                retval = _User.nickName
            End If
            Return retval
        End Get
    End Property


    Public Property ParentTitle As DTOLangText
        Get
            If _ParentTitle Is Nothing Then
                Dim oNoticia As New DTONoticia(_Parent)
                _ParentTitle = oNoticia.title
            End If
            Return _ParentTitle
        End Get
        Set(value As DTOLangText)
            _ParentTitle = value
        End Set
    End Property


    Shared Function HtmlText(oComment As DTOPostComment) As String
        Dim src As String = oComment.Text
        Dim retval As String = src.Replace(vbCrLf, "<br/>")
        Return retval
    End Function

    Shared Function Noticia(oPostComment As DTOPostComment) As DTONoticia
        Dim retval As New DTONoticia(oPostComment.Parent)
        Return retval
    End Function

    Shared Function UserNickname(oPostComment As DTOPostComment) As String
        Dim oUser As DTOUser = oPostComment.User
        Dim retval As String = oUser.nickName
        If retval = "" Then retval = oUser.nom
        If retval = "" Then retval = oUser.emailAddress.Substring(0, oUser.emailAddress.IndexOf("@") - 1)
        Return retval
    End Function

    Shared Function Tree(items As List(Of DTOPostComment)) As List(Of DTOPostComment)
        'construeix arbre de comentaris a partir de una llista plana

        Dim retval As New List(Of DTOPostComment)
        For Each oItem As DTOPostComment In items
            If oItem.Answering Is Nothing Then
                'afageix-lo com a comentari arrel de conversa
                retval.Insert(0, oItem)
            Else
                'assigna les respostes als seus comentaris respectius
                For Each oTarget As DTOPostComment In items
                    If oTarget.Equals(oItem.Answering) Then
                        oTarget.Answers.Add(oItem)
                    End If
                Next
            End If
        Next

        Return retval
    End Function
End Class



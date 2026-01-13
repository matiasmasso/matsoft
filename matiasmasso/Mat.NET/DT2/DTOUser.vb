Public Class DTOUser
    Inherits DTOBaseTel

    Property emp As DTOEmp
    Property nickName As String
    Property nom As String
    Property cognoms As String
    Property sex As Sexs = Sexs.NotSet
    Property birthYear As Integer
    Property birthday As Date
    Property childCount As Integer
    Property lastChildBirthday As Date
    Property adr As String
    Property zipCod As String
    Property location As DTOLocation
    Property locationNom As String
    Property provinciaNom As String
    Property country As DTOCountry
    Property tel As String
    Property password As String
    Property lang As DTOLang
    Property eFras As Boolean
    Property source As Sources
    Property rol As DTORol
    Property contact As DTOContact
    Property contacts As List(Of DTOContact)
    Property privat As Boolean
    Property noNews As Boolean
    Property subscriptions As List(Of DTOSubscription)
    Property badMail As DTOEmail.BadMailErrs
    Property obsoleto As Boolean
    Property fchCreated As Date
    Property fchActivated As Date
    Property fchDeleted As Date

    Property validationResult As ValidationResults

    Property emailAddress As String
        Get
            Return MyBase.value
        End Get
        Set(value As String)
            MyBase.value = value
        End Set
    End Property

    Public Enum wellknowns
        info
        matias
        victoria
        toni
        ZabalaHoyos
        Rosillo
        CarlosRuiz
        Enric
        Xavi
        Eric
        TraquinaPerfeito
    End Enum

    Public Enum Sexs
        NotSet
        Male
        Female
        NotApplicable
    End Enum

    Public Enum Sources
        NotSet
        Website
        Blog
        WpFollower
        WpComment
        Manual
        WebComment
        Raffle
        Facebook
        Fb4moms
        iMat
        External
        Spv
    End Enum

    Public Enum ValidationResults
        NotSet
        Success
        EmptyEmail
        WrongEmail
        EmailNotRegistered
        EmptyPassword
        WrongPassword
        NewValidatedUser
        SystemError
        UserDeleted
        NotAuthorized
        WrongZip
    End Enum


    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        MyBase.ObjCod = DTOBaseTel.ObjCods.User
        _Subscriptions = New List(Of DTOSubscription)
    End Sub

    Public Sub New() 'Constructor sense parametres per serialitzar-lo al pujar les dades via Ajax per exemple de Quiz
        MyBase.New()
        MyBase.ObjCod = DTOBaseTel.ObjCods.User
        _Subscriptions = New List(Of DTOSubscription)
    End Sub

    Shared Function Factory(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional sFullEmailAddress As String = "") As DTOUser
        Dim oLang As DTOLang = DTOLang.ESP
        Dim retval As New DTOUser()
        With retval
            .Emp = oEmp
            If oContact Is Nothing Then
                .Lang = DTOLang.ESP
            Else
                .Contacts = {oContact}.ToList
                .Contact = oContact
                .Rol = oContact.Rol
                .Lang = oContact.Lang
            End If

            If Not String.IsNullOrEmpty(sFullEmailAddress) Then
                Dim pattern = "(\w[\w.-]+@)+\w[\w.-]+"
                Dim r As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                Dim oMatch As System.Text.RegularExpressions.Match = r.Match(sFullEmailAddress)
                .EmailAddress = oMatch.Value.ToLower

                pattern = "<.*?>"
                .Obs = Text.RegularExpressions.Regex.Replace(sFullEmailAddress, pattern, String.Empty)
            End If

            .Source = DTOUser.Sources.Manual
        End With

        Return retval
    End Function

    Shared Function GetEmailAddress(oUser As DTOUser) As String
        Dim retval As String = ""
        If oUser IsNot Nothing Then
            retval = oUser.emailAddress
        End If
        Return retval
    End Function

    Shared Function IsAuthenticated(oUser As DTOUser) As Boolean
        Dim retval As Boolean
        If oUser IsNot Nothing Then
            If oUser.rol IsNot Nothing Then
                retval = oUser.rol.IsAuthenticated
            End If
        End If
        Return retval
    End Function

    Public Shadows Function Trim() As DTOUser
        Dim retval As New DTOUser(MyBase.Guid)
        Return retval
    End Function

    Public Function NicknameOrElse() As String
        Dim retval As String = _NickName
        If retval = "" Then retval = String.Format("{0} {1}", _Nom, _Cognoms).Trim
        If retval = "" Then retval = Me.EmailAddress
        Return retval
    End Function

    Shared Function NicknameOrElse(oUser As DTOUser) As String
        Dim retval As String = ""
        If oUser IsNot Nothing Then
            retval = oUser.NicknameOrElse
        End If
        Return retval
    End Function

    Shared Function AddressAndNickname(oUser As DTOUser) As String
        Dim sb As New Text.StringBuilder
        If oUser IsNot Nothing Then
            sb.Append(oUser.EmailAddress)
            If oUser.NickName > "" Then
                sb.Append(" " & oUser.NickName)
            End If
            If oUser.Nom > "" Or oUser.Nom > "" Then
                sb.Append(" " & oUser.Nom & " " & oUser.Cognoms)
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function Nom_i_Cognoms(oUser As DTOUser) As String
        Dim sb As New System.Text.StringBuilder
        If oUser.Nom > "" Then
            sb.Append(oUser.Nom)
            If oUser.Cognoms > "" Then
                sb.Append(" ")
                sb.Append(oUser.Cognoms)
            End If
        Else
            If oUser.Cognoms > "" Then
                sb.Append(oUser.Cognoms)
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function wellknown(id As wellknowns) As DTOUser
        Dim retval As DTOUser = Nothing
        Dim sGuid As String = ""
        Select Case id
            Case DTOUser.wellknowns.info
                sGuid = "A117BF28-CADF-439E-B3B2-575B9AC615B4"
            Case DTOUser.wellknowns.victoria
                sGuid = "B166BB33-C277-4FC4-B1FF-F7713C101215"
            Case DTOUser.wellknowns.matias
                sGuid = "961297AF-BC62-44ED-932A-2C445B7D69C3"
            Case DTOUser.wellknowns.toni
                sGuid = "5FA9EE85-02D2-415A-AB30-A015A240CD13"
            Case DTOUser.wellknowns.ZabalaHoyos
                sGuid = "9512706E-06AF-4859-B4AE-D639DEC471A7"
            Case DTOUser.wellknowns.Rosillo
                sGuid = "7AC3B5CD-C0EB-40C3-820B-5D3FE44ABF05"
            Case DTOUser.wellknowns.CarlosRuiz
                sGuid = "0BFC6E6C-1E78-48ED-B105-B16A19869840"
            Case wellknowns.Enric
                sGuid = "38D5EC9D-B830-478E-9CB6-6C7945F4BA82"
            Case DTOUser.wellknowns.Xavi
                sGuid = "FA89CF75-71C9-48EA-BC20-B882C6C6FED7"
            Case DTOUser.wellknowns.Eric
                sGuid = "79C24788-A7E4-4520-9804-D95DDFDE915F"
            Case wellknowns.TraquinaPerfeito
                sGuid = "8501C51D-EEC8-44C3-B3CE-1468F673354D"
        End Select

        If sGuid > "" Then
            Dim oGuid As New Guid(sGuid)
            retval = New DTOUser(oGuid)
        End If
        Return retval
    End Function

    Public Function DisplayObs() As String
        'especific per acompanyar la asdreça email a Xl_Tels
        Dim retval As String = _NickName

        If MyBase.Obs > "" Then
            If retval > "" Then retval += " "
            retval = String.Format("{0} ({1})", retval, MyBase.Obs)
        End If
        Return retval
    End Function

    Shared Function Anonymous(oEmp As DTOEmp) As DTOUser
        Dim retval = DTOUser.Factory(oEmp)
        With retval
            .Rol = New DTORol(DTORol.Ids.Unregistered)
            .Lang = DTOLang.ESP
        End With
        Return retval
    End Function

    Shared Function CheckEmailSintaxis(sEmail As String) As Boolean
        Dim retval As Boolean = True
        If sEmail.Contains("@") Then
            Dim segments() As String = sEmail.Split("@")
            If segments(0).Length < 1 Then retval = False
            Dim DomainSegments() As String = segments(1).Split(".")
            If DomainSegments.Length < 2 Then retval = False
            If DomainSegments.Last.Length < 2 Then retval = False
        Else
            retval = False
        End If

        Return retval
    End Function



    Public Shared Function IsEmailNameAddressValid(src As String) As Boolean
        Dim retval As Boolean
        Try
            Dim email As New System.Net.Mail.MailAddress(src)
            retval = True
        Catch ex As Exception

        End Try
        Return retval
    End Function

    Shared Function GetBirthYear(oUser As DTOUser) As Integer
        Dim retval As Integer
        If oUser.BirthYear > 0 Then
            retval = oUser.BirthYear
        ElseIf oUser.Birthday <> Nothing Then
            retval = oUser.Birthday.Year
        End If
        Return retval
    End Function

    Shared Sub Merge(ByRef target As DTOUser, source As DTOUser)
        With target
            .Guid = source.Guid
            .EmailAddress = source.EmailAddress
            .NickName = source.NickName
            .Nom = source.Nom
            .Rol = source.Rol
            .Password = source.Password
            .BadMail = source.BadMail
            .NoNews = source.NoNews
            .Contacts.AddRange(source.Contacts.Except(target.Contacts))
        End With
    End Sub

    Shared Function VerificationCode(sEmail As String) As String
        'test@test.com 746b4
        Dim oBytes() As Byte = FileSystemHelper.GetStreamFromString(sEmail)
        Dim sHash As String = CryptoHelper.HashMD5(oBytes)
        Dim sCodi As String = CryptoHelper.StringToHexadecimal(sHash)
        Dim retval As String = sCodi.Substring(0, 5)
        Return retval
    End Function

    Shared Function AllowContactBrowse(oUser As DTOUser, oContact As DTOContact) As Boolean
        Dim retval As Boolean
        If oContact Is Nothing Then
            retval = True
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Auditor
                    retval = True
                Case Else
                    retval = Not oContact.Rol.IsStaff
            End Select
        End If
        Return retval
    End Function


    Shared Function IsUserAllowedToRead(oUser As DTOUser, oTargetRol As DTORol) As Boolean
        Dim retval As Boolean = False

        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Auditor
                retval = True
            Case DTORol.Ids.LogisticManager, DTORol.Ids.Accounts
                If oTargetRol IsNot Nothing Then
                    Select Case oTargetRol.Id
                        Case DTORol.Ids.CliFull, DTORol.Ids.CliLite, DTORol.Ids.Rep, DTORol.Ids.Manufacturer, DTORol.Ids.Marketing
                            retval = True
                    End Select
                End If
            Case DTORol.Ids.SalesManager
                If oTargetRol IsNot Nothing Then
                    Select Case oTargetRol.Id
                        Case DTORol.Ids.CliFull, DTORol.Ids.CliLite, DTORol.Ids.Rep, DTORol.Ids.Comercial
                            retval = True
                    End Select
                End If
            Case DTORol.Ids.LogisticManager, DTORol.Ids.Operadora, DTORol.Ids.Marketing
                If oTargetRol IsNot Nothing Then
                    Select Case oTargetRol.Id
                        Case DTORol.Ids.CliFull, DTORol.Ids.CliLite, DTORol.Ids.Rep
                            retval = True
                    End Select
                End If
        End Select
        Return retval
    End Function

    Shared Function IsStaff(oUser As DTOUser) As Boolean
        Dim retval As Boolean
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Accounts,
                 DTORol.Ids.Comercial, DTORol.Ids.LogisticManager, DTORol.Ids.Marketing, DTORol.Ids.Operadora,
                  DTORol.Ids.Taller
                retval = True
        End Select
        Return retval
    End Function


End Class

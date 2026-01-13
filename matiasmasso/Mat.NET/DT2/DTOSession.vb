Public Class DTOSession
    Inherits DTOBaseGuid

    Property AppType As DTOApp.AppTypes
    Property Emps As List(Of DTOEmp)
    Property Emp As DTOEmp
    Property Cur As DTOCur
    Property Lang As DTOLang
    Property Rol As DTORol
    Property User As DTOUser
    Property Contact As DTOContact
    Property IsAuthenticated As Boolean
    Property FchFrom As Date
    Property FchTo As Date
    Property Culture As String

    Property Incidencia As DTOIncidencia

    Public Const CookieSessionNameBackup As String = "UserSessionBackup"
    Public Const CookieSessionName As String = "UserSession"
    Public Const CookiePersistName As String = "UserPersist"
    Public Const CookiesAccepted As String = "CookiesAccepted"

    Public Enum CookieIds
        None
        UserSession
        UserPersist
        CookiesAccepted
        LastProductBrowsed
        LastSessionStart
        LastSessionEnd
    End Enum


    Public Enum Settings
        none
        User_Persisted
        FrmIdx_Width
        FrmIdx_Height
        FrmIdx_Splitter
        Last_Menu_Selection
        Last_Balance_Fch
        Last_Product_Selected
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Sub New(EmpId As DTOEmp.Ids)
        MyBase.New()
        _Emp = New DTOEmp(EmpId)
    End Sub

    Shared Function Factory(sCulture As String, Optional oUser As DTOUser = Nothing) As DTOSession
        'requires loaded User
        Dim exs As New List(Of Exception)
        Dim retval As New DTOSession()
        With retval
            .Culture = sCulture
            .FchFrom = Now
            If oUser Is Nothing Then
                .Rol = New DTORol(DTORol.Ids.Unregistered)
                .Lang = DTOLang.FromCulture(sCulture)
            Else
                .User = oUser
                .Rol = oUser.Rol
                .Lang = oUser.Lang
            End If
        End With
        Return retval
    End Function



    Shared Function GetIsAuthenticated(oSession As DTOSession) As Boolean
        Dim retval As Boolean
        If oSession IsNot Nothing Then
            Dim oUser As DTOUser = oSession.User
            Dim oRol As DTORol = oUser.Rol
            retval = oRol.IsAuthenticated
        End If
        Return retval
    End Function

    Public Function tradueix(Esp As String, Cat As String, Eng As String, Optional Por As String = "") As String
        Dim retval As String = Esp
        If _Lang IsNot Nothing Then
            retval = _Lang.tradueix(Esp, Cat, Eng, Por)
        End If
        Return retval
    End Function

    Public Sub LogOff()
        _IsAuthenticated = False
        _Incidencia = Nothing
    End Sub
End Class

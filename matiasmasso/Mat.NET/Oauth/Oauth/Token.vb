Imports Newtonsoft.Json

Public Class Token
    <JsonProperty("access_token")>
    Public Property AccessToken() As String
        Get
            Return m_AccessToken
        End Get
        Set
            m_AccessToken = Value
        End Set
    End Property
    Private m_AccessToken As String

    <JsonProperty("token_type")>
    Public Property TokenType() As String
        Get
            Return m_TokenType
        End Get
        Set
            m_TokenType = Value
        End Set
    End Property
    Private m_TokenType As String

    <JsonProperty("expires_in")>
    Public Property ExpiresIn() As Integer
        Get
            Return m_ExpiresIn
        End Get
        Set
            m_ExpiresIn = Value
        End Set
    End Property
    Private m_ExpiresIn As Integer

    <JsonProperty("refresh_token")>
    Public Property RefreshToken() As String
        Get
            Return m_RefreshToken
        End Get
        Set
            m_RefreshToken = Value
        End Set
    End Property
    Private m_RefreshToken As String
End Class
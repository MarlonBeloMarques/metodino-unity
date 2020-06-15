using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Facebook.MiniJSON;
using System;

public class FacebookAndPlayFabFunctions : MonoBehaviour
{
    //Title Id do seu jogo no site do PlayFab
    public string gameTitleId = "";
    public Text statustxt;
    public GameObject login_imag;
    public GameObject aviso_comparth;
    public GameObject aviso_rank;
    public GameObject aviso_saverank;
    public GameObject aviso_sucesslogin;
    public GameObject aviso_failedlogin;
    public Text failedlogintxt;
    public Text saveranktxt;
    public static bool aviso_restricao;

    void Awake()
    {
        login_imag.SetActive(FacebookAndPlayFabInfo.login_imag);
        statustxt.text = FacebookAndPlayFabInfo.status;

        if(!FB.IsInitialized)
        {
            //Usado para inicializar o sdk do facebook.
            FB.Init(InitCallback, OnHideUnity);
            //Usado para indicar ao sdk do PlayFab o Id do seu jogo.
            PlayFabSettings.TitleId = gameTitleId;
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void OnHideUnity(bool IsGameShown)
    {
        if(!IsGameShown)
        {
            Time.timeScale = 0;
        }
        else
        { 
            Time.timeScale = 1;
        }
    }

    private void InitCallback()
    {
        //Verifica se foi possível inicializar o sdk do facebook.
        if (FB.IsInitialized)
        {
            //Ativa o sdk do facebook no jogo.
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to initialize the facebook sdk");
        }
    }

    //Usado para realizar o login no facebook e no playfab ao mesmo tempo.
    public void Login()
    {
        // Usado para verificar se o usuário já está logado, caso não esteja ele tenta logar.
        if (!FacebookAndPlayFabInfo.isLoggedOnPlayFab)
        {
            //Cria a lista de permissões que o aplicativo utilizará, essas são as permissões padrão.
            List<string> permissions = new List<string>() { "public_profile", "email", "user_friends" };

            //Utiliza o SDK do Facebook para realizar o login, utilizando as permissões e indicando a função de callback.
            FB.LogInWithReadPermissions(permissions, LoginFacebookCallBack);
        }
    }

    private void LoginFacebookCallBack(ILoginResult loginResult)
    {
        //Caso o resultado seja nulo, deu algum erro no login.
        if (loginResult == null)
        {
            Debug.Log("Nao foi possivel logar no facebook.");
            aviso_failedlogin.SetActive(true);
            failedlogintxt.text = "Nao foi possivel logar no facebook. Erro 1.";
            aviso_restricao = true;
            return;
        }

        //Verifica se o retorno não foi um erro, ou algum tipo de cancelamento caso não seja nenhum desses tipos indica
        //que foi possível realizar o login com o facebook com sucesso.
        if (string.IsNullOrEmpty(loginResult.Error) || !loginResult.Cancelled || !string.IsNullOrEmpty(loginResult.RawResult))
        {
            Debug.Log("Sucesso ao Logar no Facebook.");

            //Aqui verificamos se o usuário já está logado no PlayFab e caso não esteja tenta realizar o login.
            if (!FacebookAndPlayFabInfo.isLoggedOnPlayFab)
            {
                //O PlayFab possuí vários tipos de login, neste artigo vamos utilizar o Login com o facebook
                //então nessa parte configuramos uma chamada para o PlayFab se Logar através dos dados do usuário
                //no facebook.
                LoginWithFacebookRequest loginFacebookRequest = new LoginWithFacebookRequest()
                {
                    //Indica o TitleId do seu jogo no PlayFab.
                    TitleId = PlayFabSettings.TitleId,
                    //Indica o token de acesso do usuário no Facebook, esse token só é gerado se o usuário já estiver
                    //logado no facebook
                    AccessToken = AccessToken.CurrentAccessToken.TokenString,
                    //Indica para criar uma conta automaticamente dentro do seu jogo no PlayFab para este usuário, 
                    //caso ele já não possua uma
                    CreateAccount = true
                };

                //Utiliza o SDK do PlayFab para realizar o login, utilizando a chamada e indicando as funções de callback
                //de sucesso e de error.
                PlayFabClientAPI.LoginWithFacebook(loginFacebookRequest, PlayFabLoginSucessCallBack, PlayFabErrorCallBack);
            }
        }
        else
        {
            Debug.Log("Nao foi possivel logar no facebook.");
            aviso_failedlogin.SetActive(true);
            failedlogintxt.text = "Nao foi possivel logar no facebook. Erro 2.";
            aviso_restricao = true;
        }
    }

    private void PlayFabLoginSucessCallBack(PlayFab.ClientModels.LoginResult playfabLoginResult)
    {
        Debug.Log("Sucesso ao Logar no PlayFab.");

        //Armazena o Id do PlayFab na classe estática e com isso é possível utilizar esses dados em outros scripts.
        FacebookAndPlayFabInfo.userPlayFabId = playfabLoginResult.PlayFabId;

        //Utiliza o SDK do Facebook para consultar os dados, indicando a função de callback. O valor "/me" indica
        //que estou buscando os dados do usuário que está logado. O valor HttpMethod.GET indica que a nossa chamada ao
        //facebook tem a intenção de somente coletar dados.
        FB.API("/me", HttpMethod.GET, CollectLoggedUserInfoCallback);
    }

    private void CollectLoggedUserInfoCallback(IGraphResult result)
    {
        //Caso o resultado seja nulo, deu algum erro ao coletar os dados.
        if (result == null)
        {
            Debug.Log("Não foi possível coletar os dados do usuário no Facebook.");
            aviso_failedlogin.SetActive(true);
            failedlogintxt.text = "Nao foi possivel coletar os dados do usuario no Facebook. Erro 3.";
            aviso_restricao = true;
            return;
        }

        //Verifica se o retorno não foi um erro, ou algum tipo de cancelamento caso não seja nenhum desses tipos indica
        //que foi possível coletar os dados do facebook com sucesso.
        if (string.IsNullOrEmpty(result.Error) && !result.Cancelled && !string.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log("Sucesso ao coletar os dados da conta do usuário no Facebook");

            try
            {
                //A resposta do Facebook vem em formato de Json e com isso nós convertemos o Json para um Dictionary
                //para ser mais facil de coletar os dados
                Dictionary<string, object> dict = Json.Deserialize(result.RawResult) as Dictionary<string, object>;
                string userFacebookName = dict["name"] as string;
                string userFacebookId = dict["id"] as string;

                //Exibe o Json de resposta no console da Unity
                Debug.Log(dict.ToJson());

                //Chamada usada para atualizar o nome do Usuario no playFab com o id do Facebook. Isto irá permitir 
                //que futuramente seja possível coletar as informações desse usuário ao exibir o resultado do Leaderboard.
                //Neste ponto foi utilizado o Id do Facebook ao invés do Nome do usuário no Facebook porque o campo
                //Display Name no PlayFab deve ser único e não passar de 25 caracters. Sendo assim o Id do facebook
                //nos atende muito bem pois ele é um id único por usuário e não ultrapassa esse limite.
                UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest();
                request.DisplayName = userFacebookId;

                PlayFabClientAPI.UpdateUserTitleDisplayName(request, UpdateUserTitleDisplayNameSucessCallback, PlayFabErrorCallBack);

                //Atualiza as informações da classe estática indicando que o usuário está logado no PlayFab
                //e informando o Id do facebook e o nome do usuário. Com isso é possível utilizar esses dados em outros scripts.
                FacebookAndPlayFabInfo.isLoggedOnPlayFab = true;
                FacebookAndPlayFabInfo.userFacebookId = userFacebookId;
                FacebookAndPlayFabInfo.userName = userFacebookName;
            }
            //Usado caso o Facebook não tenha retornado o id ou o nome do usuário.
            catch (KeyNotFoundException e)
            {
                Debug.Log("Não foi possível coletar os dados do usuário no Facebook. Erro: " + e.Message);
                aviso_failedlogin.SetActive(true);
                failedlogintxt.text = "Nao foi possível coletar os dados do usuario no Facebook. Erro: " + e.Message;
            }
        }
        else
        {
            Debug.Log("Não foi possível coletar os dados do usuário no Facebook.");
            aviso_failedlogin.SetActive(true);
            failedlogintxt.text = "Nao foi possivel coletar os dados do usuario no Facebook. Erro 4.";
            aviso_restricao = true;
        }
    }

    private void UpdateUserTitleDisplayNameSucessCallback(UpdateUserTitleDisplayNameResult result)
    {
        //Exibe no console da Unity que atualizou o campo com sucesso.
        Debug.Log("O campo Display Name do usuário no PlayFab foi atualizado com sucesso.");
        aviso_sucesslogin.SetActive(true);
        StatusNet("Conectado");
        statustxt.text = FacebookAndPlayFabInfo.status;
        aviso_restricao = true;

    }

    private void PlayFabErrorCallBack(PlayFabError playfabError)
    {
        //Exibe no console da Unity as informações do erro.
        Debug.Log(playfabError.ErrorMessage);
        Debug.Log(playfabError.ErrorDetails);
    }

    public void UpdateScore(int score)
    {
        score = PlayerPrefs.GetInt("Record");

        //Verifica se o usuário está logado no PlayFab, pois só é possível alterar o score caso ele esteja logado.
        if (FacebookAndPlayFabInfo.isLoggedOnPlayFab)
        {
            //Usado para passar o valor para o PlayFab. O nome do campo Score deve ser identico ao usado no Leadeboard
            //no site do PlayFab. Caso quisesssem armazenar o nome do jogador poderiam criar um Dictionary<string, string>
            List<StatisticUpdate> stats = new List<StatisticUpdate>();
            //Ao adicionar o valor para o servidor sempre é preciso definir uma chave ( nesse caso "Score") para identificar
            //o que é o dado que você está passando e o outro parâmetro é o valor.
            //PlayFab.ClientModels.StatisticUpdate
            StatisticUpdate objeto = new StatisticUpdate();

            objeto.StatisticName = "Score";
            objeto.Value = score;

            stats.Add(objeto);

            //Configura a chamada para atualizar o score no servidor.
            UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest();
            request.Statistics = stats;

            //Utiliza o SDK do PlayFab para atualizar os dados. Informando a chamada e a função de callback de sucesso e erro.
            PlayFabClientAPI.UpdatePlayerStatistics(request, UpdateUserStatisticsSucessCallback, PlayFabErrorCallBack);

            if (!aviso_restricao)
            {
                aviso_saverank.SetActive(true);
                saveranktxt.text = "Record salvado no Ranking com Sucesso.";
            }
            Debug.Log("Record salvado no Ranking com Sucesso.");
        }
        else
        {
            if (!aviso_restricao)
            {
                aviso_saverank.SetActive(true);
            }
                Debug.Log("Não é possível alterar o Score sem estar logado.");
        }
    }

    private void UpdateUserStatisticsSucessCallback(UpdatePlayerStatisticsResult updateUserStatisticsResult)
    {
        //Exibe no console da Unity que o dado foi alterado no PlayFab com sucesso.
        Debug.Log("Sucesso ao inserir/atualizar o Score do jogador no PlayFab.");
    }

    public void LoadLeaderBoard()
    {
        //Verifica se o usuário está logado pois só é possível carregar os dados do Leadeboard caso ele esteja logado.
        if (FacebookAndPlayFabInfo.isLoggedOnPlayFab)
        {
            //Configura a chamada para carregar os dados do Leaderboard. Informa qual é a o nome do Leaderboard e quantos
            //resultados deve trazer no máximo.
            GetLeaderboardRequest request = new GetLeaderboardRequest();
            request.MaxResultsCount = 10;
            request.StatisticName = "Score";

            //Atualiza a váriavel para falso indicando que os dados ainda não foram carregados.
            FacebookAndPlayFabInfo.leaderboardLoaded = false;
            //Atualiza a váriavel para verdadeiro indicando que os dados estão sendo carregados.
            FacebookAndPlayFabInfo.leaderboardIsLoading = true;

            //As variáveis acima podem ser usadas em outros scripts que precisem esperar até os dados serem carregados ou 
            //identificar quando começou a carregar para realizar alguma ação dentro do seu jogo.

            //Utiliza o SDK do PlayFab para coletar os dados. Informando a chamada e a função de callback de sucesso e erro.
            PlayFabClientAPI.GetLeaderboard(request, GetLeaderboardSucessCallback, PlayFabErrorCallBack);
        }
        else
        {
            if (!aviso_restricao)
            {
                aviso_rank.SetActive(true);
            }
                Debug.Log("Não é possível carregar os Dados do Leadeboard sem está logado.");
        }
    }

    private void GetLeaderboardSucessCallback(GetLeaderboardResult result)
    {
        //Verifica se o resultado não veio nulo.
        if (result != null && result.Leaderboard != null)
        {
            //Pega o objeto que representa o Leaderboard.
            GameObject leaderboardGameObject = GameObject.Find("Leadeboard");

            //Percorre cada uma das 5 linhas que podem vir no Leaderboard e atualiza os campos na cena da Unity
            foreach (PlayerLeaderboardEntry leadeboardLine in result.Leaderboard)
            {
                GetUserInfoAndUpdateLeaderboard(leadeboardLine, leaderboardGameObject, result);
            }
        }
    }

    private void GetUserInfoAndUpdateLeaderboard(PlayerLeaderboardEntry leaderBoardLine, GameObject leaderboardGameObject, GetLeaderboardResult result)
    {
        //Coleta o id do Facebook do usuário que está no rank através do Display Name que atualizamos ao usuário efetuar o login.
        string userFacebookId = leaderBoardLine.DisplayName;

        //Utiliza o SDK do Facebook para coletar as informações do usuário. O valor "/" + userFacebookId serve
        //para indicar de qual usuário queremos os dados. O valor HttpMethod.GET indica que a nossa chamada ao
        //facebook tem a intenção de somente coletar dados.
        FB.API("/" + userFacebookId, HttpMethod.GET,
        //Esta é uma outra forma de criar uma função de callback, o userInfoResult é a váriavel que
        //recebe o resultado que é passado para a função de callback
        (userInfoResult) =>
        {
            //Caso o resultado seja nulo, deu algum erro ao coletar os dados.
            if (userInfoResult == null)
            {
                Debug.Log("Não foi possível coletar os dados do usuário no Facebook.");

                //Verifica se está no ultimo registro do Leadeboard. Para indicar que finalizou o carregamento.
                if (leaderBoardLine.Position + 1 == result.Leaderboard.Count)
                {
                    //Atualiza a váriavel para verdadeiro indicando que os dados ainda já foram carregados.
                    FacebookAndPlayFabInfo.leaderboardLoaded = true;
                    //Atualiza a váriavel para falso indicando que os dados estão já foram carregados.
                    FacebookAndPlayFabInfo.leaderboardIsLoading = false;

                    Debug.Log("Os dados do Leaderboard foram carregados.");
                }

                return;
            }

            //Verifica se o retorno não foi um erro, ou algum tipo de cancelamento caso não seja nenhum desses tipos indica
            //que foi possível coletar os dados do facebook com sucesso.
            if (string.IsNullOrEmpty(userInfoResult.Error) && !userInfoResult.Cancelled && !string.IsNullOrEmpty(userInfoResult.RawResult))
            {
                Debug.Log("Sucesso ao coletar os dados da conta do usuário no Facebook");

                try
                {
                    //A resposta do Facebook vem em formato de Json e com isso nós convertemos o Json para um Dictionary
                    //para ser mais facil de coletar os dados
                    Dictionary<string, object> dict = Json.Deserialize(userInfoResult.RawResult) as Dictionary<string, object>;
                    string userFacebookName = dict["name"] as string;

                    //Pega o objeto que possui as informações de um jogador no Leaderboard.
                    GameObject playerLeadeboardGameObject = leaderboardGameObject.transform.Find("Player ( " + (leaderBoardLine.Position + 1) + " )").gameObject;

                    //Atualiza o texto com a posiçao do jogador no Leaderboard.
                    playerLeadeboardGameObject.transform.Find("Position").GetComponent<Text>().text = (leaderBoardLine.Position + 1).ToString();

                    //Atualiza o texto com o nome do jogador no Facebook.
                    playerLeadeboardGameObject.transform.Find("Name").GetComponent<Text>().text = userFacebookName;

                    //Atualiza o texto com o valor do Score do jogador no Leaderboard.
                    playerLeadeboardGameObject.transform.Find("Score").GetComponent<Text>().text = leaderBoardLine.StatValue.ToString();

                    //Verifica se está no ultimo registro do Leadeboard. Para indicar que finalizou o carregamento.
                    if (leaderBoardLine.Position + 1 == result.Leaderboard.Count)
                    {
                        //Atualiza a váriavel para verdadeiro indicando que os dados ainda já foram carregados.
                        FacebookAndPlayFabInfo.leaderboardLoaded = true;
                        //Atualiza a váriavel para falso indicando que os dados estão já foram carregados.
                        FacebookAndPlayFabInfo.leaderboardIsLoading = false;

                        Debug.Log("Os dados do Leaderboard foram carregados.");
                    }
                }
                //Usado caso o Facebook não tenha retornado o id ou o nome do usuário.
                catch (KeyNotFoundException e)
                {
                    //Verifica se está no ultimo registro do Leadeboard. Para indicar que finalizou o carregamento.
                    if (leaderBoardLine.Position + 1 == result.Leaderboard.Count)
                    {
                        //Atualiza a váriavel para verdadeiro indicando que os dados ainda já foram carregados.
                        FacebookAndPlayFabInfo.leaderboardLoaded = true;
                        //Atualiza a váriavel para falso indicando que os dados estão já foram carregados.
                        FacebookAndPlayFabInfo.leaderboardIsLoading = false;

                        Debug.Log("Os dados do Leaderboard foram carregados.");
                    }

                    Debug.Log("Não foi possível coletar os dados do usuário no Facebook. Erro: " + e.Message);
                }
            }
            else
            {
                //Verifica se está no ultimo registro do Leadeboard. Para indicar que finalizou o carregamento.
                if (leaderBoardLine.Position + 1 == result.Leaderboard.Count)
                {
                    //Atualiza a váriavel para verdadeiro indicando que os dados ainda já foram carregados.
                    FacebookAndPlayFabInfo.leaderboardLoaded = true;
                    //Atualiza a váriavel para falso indicando que os dados estão já foram carregados.
                    FacebookAndPlayFabInfo.leaderboardIsLoading = false;

                    Debug.Log("Os dados do Leaderboard foram carregados.");
                }

                Debug.Log("Não foi possível coletar os dados do usuário no Facebook.");
            }
        });
    }

    public void ShareWithFacebook()
    {
        //Verifica se o usuário está logado pois só é possível compartilhar com o facebook caso ele esteja logado.
        if (FacebookAndPlayFabInfo.isLoggedOnPlayFab)
            FB.ShareLink(contentURL: new Uri("https://www.facebook.com/Metodinomarqs/"));
        else
        {
            if (!aviso_restricao)
            {
                aviso_comparth.SetActive(true);
            }
            Debug.Log("Não é possível compartilhar com o Facebook sem está logado.");
        }
    }

    public void StatusNet(string receb)
    {
        FacebookAndPlayFabInfo.status = receb;
        FacebookAndPlayFabInfo.login_imag = true;
        login_imag.SetActive(true);
    }
}

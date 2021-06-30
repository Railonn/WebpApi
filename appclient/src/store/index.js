import { createStore } from 'vuex'

const axios = require('axios');

const instance = axios.create({
    baseURL: "http://localhost:4000/api/"
})

let user = localStorage.getItem('user');
if (!user) {
    user = {
        userId: -1,
    }
} else {
    try {
        user = JSON.parse(user);
    } catch (ex) {
        user = {
            userId: -1,
        }
    }
}

// Create a new store instance.
const store = createStore({
    state: {
        status: '',
        user: user,
        userInfos: {
            name: '',
            lastName: '',
            email: '',
            phoneNumber: '',
            adress: '',
            city: '',
            country: '',
            userServiceId: '',
            activationDone: ''
        }
    },
    mutations: {
        setStatus: function (state, status) {
            state.status = status;
        },
        logUser: async function (state, userId) {
            localStorage.setItem('user', userId);
            state.user = userId;
        },
        userInfos: function (state, userInfos) {
            state.userInfos = userInfos;
        },
        logout: function (state) {
            state.user = {
                userId: -1,
            }
            state.userInfos = '';
            localStorage.removeItem('user');
        }
    },
    actions: {
        login: ({ commit }, userInfos) => {
            commit('setStatus', 'loading');
            return new Promise((resolve, reject) => {
                instance.post("/login", userInfos)
                    .then(function (response) {
                        // Effectue une mutation vers setStatus pour changer la valeur de status
                        commit('setStatus', 'connected');
                        // Effectue une 2ème mutation vers logUser pour changer les valeurs de user
                        commit('logUser', response.data);
                        resolve(response);
                    })
                    .catch(function (error) {
                        commit('setStatus', 'error_login')
                        reject(error);
                    });
            });
        },
        createAccount: ({ commit }, userInfos) => {
            commit('setStatus', 'loading');
            return new Promise((resolve, reject) => {
                instance.post("/user", userInfos)
                    .then(function (response) {
                        commit('setStatus', 'created');
                        resolve(response);
                    })
                    .catch(function (error) {
                        commit('setStatus', 'error_create');
                        reject(error);
                    });
            });
        },
        getUserInfos: ({ commit }, user) => {
            instance.get("/user/" + user)
                .then(function (response) {
                    commit('userInfos', response.data);
                })
                .catch(function () {

                });
        }

    }
})

export default store;
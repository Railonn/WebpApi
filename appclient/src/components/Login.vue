<template>
  <div class="card">
    <h1 class="card__title" v-if="mode == 'login'">Login</h1>
    <h1 class="card__title" v-else>Sign in</h1>
    <p class="card__subtitle" v-if="mode == 'login'">
      Don't have an account yet?
      <span class="card__action" @click="switchToCreateAccount()"
        >Create an account</span
      >
    </p>
    <p class="card__subtitle" v-else>
      You already have an account?
      <span class="card__action" @click="switchToLogin()">Sign in</span>
    </p>
    <div class="form-row">
      <input
        v-model="email"
        class="form-row__input"
        type="email"
        placeholder="Email address"
        required
        autofocus
      />
    </div>
    <div class="form-row" v-if="mode == 'create'">
      <input
        v-model="name"
        class="form-row__input"
        type="text"
        placeholder="Name"
      />
      <input
        v-model="lastname"
        class="form-row__input"
        type="text"
        placeholder="LastName"
      />
    </div>
    <div class="form-row" v-if="mode == 'create'">
      <input
        v-model="phonenumber"
        @keypress="isNumber($event)"
        class="form-row__input"
        type="text"
        placeholder="Phone Number"
      />
      <input
        v-model="adress"
        class="form-row__input"
        type="text"
        placeholder="Adress"
      />
    </div>
    <div class="form-row" v-if="mode == 'create'">
      <input
        v-model="city"
        class="form-row__input"
        type="text"
        placeholder="City"
      />
      <input
        v-model="country"
        class="form-row__input"
        type="text"
        placeholder="Country"
      />
    </div>
    <div class="form-row" v-if="mode == 'create'">
      <select
        class="form-row__select"
        v-model="userserviceid"
        name="services"
        id="services-select"
      >
        <option selected="selected" disabled>Select your Service</option>
        <option value="1">Messaging</option>
        <option value="2">Documentation</option>
      </select>
    </div>
    <div class="form-row">
      <input
        v-model="password"
        class="form-row__input"
        type="password"
        placeholder="Password"
        required
      />
    </div>
    <div class="form-row" v-if="mode == 'login' && status == 'error_login'">
      Email address and/or password invalid
    </div>
    <div class="form-row" v-if="mode == 'create' && status == 'error_create'">
      Email address already in use
    </div>
    <div class="form-row">
      <button
        @click="login()"
        class="button"
        :class="{ 'button--disabled': !validateFields }"
        v-if="mode == 'login'"
      >
        <span v-if="status == 'loading'">Connection in progress...</span>
        <span v-else>Connection</span>
      </button>
      <button
        @click="createAccount()"
        class="button"
        :class="{ 'button--disabled': !validateFields }"
        v-else
      >
        <span v-if="status == 'loading'">Creation in progress...</span>
        <span v-else>Create account</span>
      </button>
    </div>
  </div>
</template>
<script>
import { mapState } from "vuex";

export default {
  name: "Login",
  data: function () {
    return {
      mode: "login",
      email: "",
      name: "",
      lastname: "",
      phonenumber: "",
      adress: "",
      city: "",
      country: "",
      userserviceid: "",
      password: "",
    };
  },
  // Vérifications des champs avant de pouvoir activer les boutons
  computed: {
    validateFields: function () {
      if (this.mode == "create") {
        if (
          this.email != "" &&
          this.name != "" &&
          this.lastname != "" &&
          this.phonenumber != "" &&
          this.adress != "" &&
          this.city != "" &&
          this.country != "" &&
          this.userserviceid != "" &&
          this.password != ""
        ) {
          return true;
        } else {
          return false;
        }
      } else {
        if (this.email != "" && this.password != "") {
          return true;
        } else {
          return false;
        }
      }
    },
    // Accés à la valeur de status dans le state
    ...mapState(["status"]),
  },
  methods: {
    // Permet de changer entre le formulaire connexion et inscription, utilisation que d'un seul composant
    switchToCreateAccount: function () {
      this.mode = "create";
    },
    switchToLogin: function () {
      this.mode = "login";
    },
    // Vérification des entrées dans l'input du numéro de téléphone
    isNumber($event) {
      let keyCode = $event.keyCode ? $event.keyCode : $event.which;
      if (keyCode < 48 || keyCode > 57) {
        $event.preventDefault();
      }
    },
    login: async function () {
      if (this.email != "" && this.password != "") {
        const self = this;
        try {
          await this.$store
            .dispatch("login", {
              email: this.email,
              password: this.password,
            })
            .then(function () {
              self.$router.push("/account");
            }),
            function (error) {
              console.log(error);
            };
        } catch (error) {
          console.log(error);
        }
      }
    },
    createAccount: function () {
      if (
        this.email != "" &&
        this.name != "" &&
        this.lastname != "" &&
        this.phonenumber != "" &&
        this.adress != "" &&
        this.city != "" &&
        this.country != "" &&
        this.userserviceid != "" &&
        this.password != ""
      ) {
        const self = this;
        // Envoie les informations vers le store pour envoyer une requête au backend
        this.$store
          .dispatch("createAccount", {
            name: this.name,
            lastname: this.lastname,
            password: this.password,
            email: this.email,
            phonenumber: this.phonenumber,
            adress: this.adress,
            city: this.city,
            country: this.country,
            activationDone: false,
            userserviceid: this.userserviceid,
          })
          .then(function () {
            // Connexion direct après enregistrement
            self.login();
          }),
          function (error) {
            console.log(error);
          };
      }
    },
  },
  mounted: function () {
    if (
      this.$store.state.user.userId !== -1 &&
      !this.$store.state.userInfos.userId
    ) {
      this.$router.push("/account");
      return;
    }
  },
};
</script>

<style scoped>
body {
  padding-top: 30px;
}
.form-row {
  display: flex;
  margin: 16px 0px;
  gap: 16px;
  flex-wrap: wrap;
}

.form-row__input {
  padding: 8px;
  border: none;
  border-radius: 8px;
  background: #f2f2f2;
  font-weight: 500;
  font-size: 16px;
  flex: 1;
  min-width: 100px;
  color: black;
}

.form-row__input:placeholder {
  color: #aaaaaa;
}

.form-row__select {
  padding: 8px;
  border: none;
  border-radius: 8px;
  background: #f2f2f2;
  font-weight: 500;
  font-size: 16px;
  flex: 1;
  min-width: 100px;
  color: black;
}
</style>
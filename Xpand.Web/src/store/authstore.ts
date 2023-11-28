import { defineStore } from "pinia";
import { ref } from "vue";

interface Captain {
  id: number;
  name: string;
  teamId: number;
}

/**
 * Just a simple store for a JWT auth
 */
export const useAuthStore = defineStore("auth", () => {
  const isLoggedIn = ref(false);
  const token = ref<string | null>(localStorage.getItem("token"));
  const captain = ref<Captain | null>(null);
  const error = ref<string | null>(null);

  // Setters
  const setLoggedIn = (value: boolean) => {
    isLoggedIn.value = value;
  };

  const setTokken = (jwtToken: string) => {
    localStorage.setItem("token", jwtToken);
    token.value = jwtToken;
  };

  const setCaptain = (cap: Captain) => {
    captain.value = cap;
  };

  // Methods -> signup is only goint to create the user, not log in
  // The user will have to log in after signing up
  const signIn = (name: string, password: string) => {
    fetch("http://localhost:5015/api/auth/signin", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ name: name, password: password }),
    })
      .then((r) => r.json())
      .then((data) => {
        console.log(data);
        if (data.token) {
          setTokken(data.token);
          setCaptain(data.captain);
          isLoggedIn.value = true;
        } else {
          error.value = data.message;
          console.log(error.value);
        }
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const signOut = async () => {
    localStorage.removeItem("token");
    fetch("http://localhost:5015/api/auth/signout", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token.value}`,
      },
    })
      .then((r) => {
        if (r.status === 200 || r.status === 401) {
          token.value = null;
          isLoggedIn.value = false;
          captain.value = null;
        } else throw new Error("Something went wrong");
      })
      .catch((err) => {
        console.error(err.message);
      });
  };

  const checkAuth = async () => {
    try {
      if (!token.value) return;
      const response = await fetch("http://localhost:5015/api/auth/info", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token.value}`,
        },
      });

      if (response.ok) {
        const data = await response.json();
        console.log(data);
        setCaptain(data.captain);
        isLoggedIn.value = true;
      } else {
        signOut();
      }
    } catch (err: any) {
      console.error(err.message);
    }
  };

  return {
    isLoggedIn,
    token,
    captain,
    setLoggedIn,
    setTokken,
    setCaptain,
    signOut,
    signIn,
    checkAuth,
  };
});

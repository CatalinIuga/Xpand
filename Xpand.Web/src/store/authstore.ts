import { defineStore } from "pinia";
import { ref } from "vue";

/**
 * Simple Pina store for a JWT Auth
 */
interface Captain {
  id: number;
  name: string;
  teamId: number;
}

export const useAuthStore = defineStore("auth", () => {
  const isLoggedIn = ref(false);
  const token = ref<string | null>(localStorage.getItem("token"));
  const captain = ref<Captain | null>(null);

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

  const signIn = async () => {
    const response = await fetch("http://localhost:5015/api/auth/signin", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ name: "test", password: "test123" }),
    });

    if (response.ok) {
      const data = await response.json();
      setTokken(data.token);
      setCaptain(data.captain);
      isLoggedIn.value = true;
    }
  };

  const signOut = async () => {
    localStorage.removeItem("token");
    fetch("http://localhost:5015/api/auth/logout", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token.value}`,
      },
    }).then(() => {
      token.value = null;
      captain.value = null;
    });
  };

  const checkAuth = async () => {
    const response = await fetch("http://localhost:5015/api/auth/info", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token.value}`,
      },
    });

    if (response.ok) {
      const data = await response.json();
      setCaptain(data.captain);
      isLoggedIn.value = true;
    } else {
      signOut();
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

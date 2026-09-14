let socket = null;
let messageHandlers = [];

function connectSocket() {
    const protocol = window.location.protocol === 'https:' ? 'wss:' : 'ws:';
    const url = `${protocol}//${window.location.host}/ws/chat`;
    console.log("Tentative de connexion au WebSocket:", url);

    socket = new WebSocket(url);

    socket.onopen = () => {
        console.log("✅ WS connecté avec succès");
    };

    socket.onerror = (e) => {
        console.error("❌ WS erreur:", e);
    };

    socket.onclose = () => {
        console.log("❌ WS déconnecté, reconnexion dans 2 secondes...");
        setTimeout(connectSocket, 2000);
    };

    socket.onmessage = (event) => {
        console.log("💬 Message reçu du serveur");
        messageHandlers.forEach(h => h(event.data));
    };
}

connectSocket();

window.RetroGalerieSocket = {
    send: (msg) => {
        if (!socket) {
            console.error("❌ Socket non initialisée");
            return;
        }
        if (socket.readyState !== WebSocket.OPEN) {
            console.error("❌ Socket non ouverte. État:", socket.readyState, 
                         "(0=CONNECTING, 1=OPEN, 2=CLOSING, 3=CLOSED)");
            return;
        }
        console.log("📤 Envoi au serveur:", msg);
        socket.send(msg);
    },
    onMessage: (callback) => {
        messageHandlers.push(callback);
    }
};

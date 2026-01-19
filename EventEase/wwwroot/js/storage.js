export function get(key) {
 return sessionStorage.getItem(key);
}

export function set(key, value) {
 sessionStorage.setItem(key, value);
}

export function remove(key) {
 sessionStorage.removeItem(key);
}

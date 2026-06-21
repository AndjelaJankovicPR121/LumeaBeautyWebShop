import { useEffect, useState } from 'react';
import axios from 'axios';
import './App.css';

function App() {
    const apiBaseUrl = 'http://localhost:5026/api';

    const [products, setProducts] = useState([]);
    const [categories, setCategories] = useState([]);
    const [brands, setBrands] = useState([]);

    const [orders, setOrders] = useState([]);
    const [showOrders, setShowOrders] = useState(false);

    const [adminOrders, setAdminOrders] = useState([]);
    const [showAdminOrders, setShowAdminOrders] = useState(false);

    const [paymentTransactions, setPaymentTransactions] = useState([]);
    const [showPaymentTransactions, setShowPaymentTransactions] = useState(false);

    const [showCheckout, setShowCheckout] = useState(false);
    const [showAuth, setShowAuth] = useState(false);

    const [user, setUser] = useState(() => {
        const savedUser = localStorage.getItem('loggedUser');
        return savedUser ? JSON.parse(savedUser) : null;
    });

    const [authMode, setAuthMode] = useState('login');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [fullName, setFullName] = useState('');
    const [address, setAddress] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');

    const [cartItems, setCartItems] = useState(() => {
        const savedCart = localStorage.getItem('cartItems');
        return savedCart ? JSON.parse(savedCart) : [];
    });

    const [wishlistItems, setWishlistItems] = useState([]);

    const [search, setSearch] = useState('');
    const [brand, setBrand] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const [sortBy, setSortBy] = useState('');

    const [pageNumber, setPageNumber] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const pageSize = 6;

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const [showAdd, setShowAdd] = useState(false);
    const [newProduct, setNewProduct] = useState({
        name: '',
        brand: '',
        description: '',
        price: '',
        stockQuantity: '',
        imageUrl: '',
        categoryId: ''
    });

    const [showEdit, setShowEdit] = useState(false);
    const [editingProduct, setEditingProduct] = useState(null);

    const getAuthConfig = () => {
        return {
            headers: {
                Authorization: `Bearer ${user?.token}`
            }
        };
    };

    const loadProducts = async () => {
        try {
            setLoading(true);
            setError('');

            const response = await axios.get(`${apiBaseUrl}/Products`, {
                params: {
                    search: search || null,
                    brand: brand || null,
                    categoryId: categoryId || null,
                    sortBy: sortBy || null,
                    pageNumber,
                    pageSize
                }
            });

            setProducts(response.data.items);
            setTotalPages(response.data.totalPages);

        } catch (err) {
            console.error(err);
            setError('Products could not be loaded.');
        } finally {
            setLoading(false);
        }
    };

    const loadBrands = async () => {
        try {
            const response = await axios.get(`${apiBaseUrl}/Products/brands`);
            setBrands(response.data);
        } catch (err) {
            console.error(err);
        }
    };

    const loadOrders = async () => {
        if (!user) return;

        try {
            const response = await axios.get(`${apiBaseUrl}/Orders/customer/${user.id}`, getAuthConfig());
            setOrders(response.data);
        } catch (err) {
            console.error(err);
            alert('Orders could not be loaded.');
        }
    };

    const loadWishlist = async () => {
        if (!user || user.role === 'Admin' || user.role === 'Employee') {
            setWishlistItems([]);
            return;
        }

        try {
            const response = await axios.get(`${apiBaseUrl}/Wishlist`, getAuthConfig());

            const productsFromWishlist = response.data
                .map(item => item.product)
                .filter(product => product != null);

            setWishlistItems(productsFromWishlist);
        } catch (err) {
            console.error(err);
            alert('Wishlist could not be loaded.');
        }
    };

    const loadAdminOrders = async () => {
        try {
            const response = await axios.get(`${apiBaseUrl}/Orders`, getAuthConfig());
            setAdminOrders(response.data);
        } catch (err) {
            console.error(err);
            alert('Orders could not be loaded.');
        }
    };

    const loadPaymentTransactions = async () => {
        try {
            const response = await axios.get(`${apiBaseUrl}/Payments/transactions`, getAuthConfig());
            setPaymentTransactions(response.data);
        } catch (err) {
            console.error(err);
            alert('Payment transactions could not be loaded.');
        }
    };

    const updateOrderStatus = async (orderId, status) => {
        try {
            await axios.put(`${apiBaseUrl}/Orders/${orderId}/status`, { status }, getAuthConfig());
            await loadAdminOrders();
            alert('Order status updated.');
        } catch (err) {
            console.error(err);
            alert('Status update failed.');
        }
    };

    useEffect(() => {
        const loadCategories = async () => {
            try {
                const response = await axios.get(`${apiBaseUrl}/Categories`);
                setCategories(response.data);
            } catch (err) {
                console.error(err);
            }
        };

        loadCategories();
        loadBrands();
    }, []);

    useEffect(() => {
        localStorage.setItem('cartItems', JSON.stringify(cartItems));
    }, [cartItems]);

    useEffect(() => {
        if (user) {
            localStorage.setItem('loggedUser', JSON.stringify(user));
            loadOrders();
            loadWishlist();
        } else {
            localStorage.removeItem('loggedUser');
            setOrders([]);
            setAdminOrders([]);
            setPaymentTransactions([]);
            setWishlistItems([]);
        }
    }, [user]);

    useEffect(() => {
        loadProducts();
    }, [search, brand, categoryId, sortBy, pageNumber]);

    useEffect(() => {
        const params = new URLSearchParams(window.location.search);
        const paymentStatus = params.get('payment');

        if (paymentStatus === 'success') {
            alert('Payment successful. Your order will appear after Stripe confirms the payment.');
            setCartItems([]);
            setShowCheckout(false);

            loadProducts();

            if (user) {
                loadOrders();
                setShowOrders(true);
            }

            window.history.replaceState({}, document.title, '/');
        }

        if (paymentStatus === 'cancelled') {
            alert('Payment was cancelled.');
            window.history.replaceState({}, document.title, '/');
        }
    }, [user]);

    const handleLogin = async () => {
        try {
            const response = await axios.post(`${apiBaseUrl}/Auth/login`, {
                username,
                password
            });

            setUser(response.data);
            setUsername('');
            setPassword('');
        } catch (err) {
            console.error(err);
            alert('Login failed. Check username and password.');
        }
    };

    const handleRegister = async () => {
        if (!username || !email || !password) {
            alert('Username, email and password are required.');
            return;
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!emailRegex.test(email)) {
            alert('Please enter a valid email address.');
            return;
        }

        if (password.length < 6) {
            alert('Password must be at least 6 characters long.');
            return;
        }

        try {
            await axios.post(`${apiBaseUrl}/Auth/register`, {
                username,
                email,
                fullName,
                address,
                phoneNumber,
                password
            });

            alert('Registration successful. You can now log in.');
            setAuthMode('login');

            setUsername('');
            setEmail('');
            setFullName('');
            setAddress('');
            setPhoneNumber('');
            setPassword('');
        } catch (err) {
            console.error(err);
            alert('Registration failed. Username or email may already exist.');
        }
    };

    const handleLogout = () => {
        setUser(null);
        setCartItems([]);
        setWishlistItems([]);
        setShowOrders(false);
        setShowAdminOrders(false);
        setShowPaymentTransactions(false);
        setShowCheckout(false);
    };

    const handleAddProduct = async () => {
        if (!newProduct.name || !newProduct.brand || !newProduct.price || newProduct.stockQuantity === '' || !newProduct.categoryId) {
            alert('Please fill in all required fields.');
            return;
        }

        if (parseFloat(newProduct.price) <= 0) {
            alert('Price must be greater than 0.');
            return;
        }

        if (parseInt(newProduct.stockQuantity) < 0) {
            alert('Stock quantity cannot be negative.');
            return;
        }

        try {
            await axios.post(`${apiBaseUrl}/Products`, {
                name: newProduct.name,
                brand: newProduct.brand,
                description: newProduct.description,
                price: parseFloat(newProduct.price),
                stockQuantity: parseInt(newProduct.stockQuantity),
                imageUrl: newProduct.imageUrl,
                categoryId: parseInt(newProduct.categoryId)
            }, getAuthConfig());

            alert('Product added successfully.');

            setNewProduct({
                name: '',
                brand: '',
                description: '',
                price: '',
                stockQuantity: '',
                imageUrl: '',
                categoryId: ''
            });

            setShowAdd(false);
            await loadProducts();
            await loadBrands();
        } catch (err) {
            console.error(err);
            alert('Error adding product.');
        }
    };

    const openEditProduct = (product) => {
        setEditingProduct({
            id: product.id,
            name: product.name,
            brand: product.brand,
            description: product.description,
            price: product.price,
            stockQuantity: product.stockQuantity,
            imageUrl: product.imageUrl,
            categoryId: product.categoryId
        });

        setShowEdit(true);
    };

    const handleEditProduct = async () => {
        if (!editingProduct) return;

        if (!editingProduct.name || !editingProduct.brand || !editingProduct.price || editingProduct.stockQuantity === '' || !editingProduct.categoryId) {
            alert('Please fill in all required fields.');
            return;
        }

        if (parseFloat(editingProduct.price) <= 0) {
            alert('Price must be greater than 0.');
            return;
        }

        if (parseInt(editingProduct.stockQuantity) < 0) {
            alert('Stock quantity cannot be negative.');
            return;
        }

        try {
            await axios.put(`${apiBaseUrl}/Products/${editingProduct.id}`, {
                id: editingProduct.id,
                name: editingProduct.name,
                brand: editingProduct.brand,
                description: editingProduct.description,
                price: parseFloat(editingProduct.price),
                stockQuantity: parseInt(editingProduct.stockQuantity),
                imageUrl: editingProduct.imageUrl,
                categoryId: parseInt(editingProduct.categoryId)
            }, getAuthConfig());

            alert('Product updated successfully.');

            setShowEdit(false);
            setEditingProduct(null);

            await loadProducts();
            await loadBrands();
        } catch (err) {
            console.error(err);
            alert('Error updating product.');
        }
    };

    const deleteProduct = async (productId) => {
        const confirmed = window.confirm('Are you sure you want to delete this product?');

        if (!confirmed) return;

        try {
            await axios.delete(`${apiBaseUrl}/Products/${productId}`, getAuthConfig());

            alert('Product deleted successfully.');

            await loadProducts();
            await loadBrands();
        } catch (err) {
            console.error(err);
            alert('Product cannot be deleted or an error occurred.');
        }
    };

    const resetFilters = () => {
        setSearch('');
        setBrand('');
        setCategoryId('');
        setSortBy('');
        setPageNumber(1);
    };

    const addToCart = (product) => {
        const existingItem = cartItems.find(item => item.id === product.id);

        if (existingItem) {
            if (existingItem.quantity >= product.stockQuantity) {
                alert('There is not enough stock for this product.');
                return;
            }

            setCartItems(cartItems.map(item =>
                item.id === product.id
                    ? { ...item, quantity: item.quantity + 1 }
                    : item
            ));
        } else {
            setCartItems([...cartItems, { ...product, quantity: 1 }]);
        }
    };

    const increaseQuantity = (productId) => {
        setCartItems(cartItems.map(item => {
            if (item.id === productId) {
                if (item.quantity >= item.stockQuantity) {
                    alert('There is not enough stock for this product.');
                    return item;
                }

                return { ...item, quantity: item.quantity + 1 };
            }

            return item;
        }));
    };

    const decreaseQuantity = (productId) => {
        setCartItems(cartItems
            .map(item =>
                item.id === productId
                    ? { ...item, quantity: item.quantity - 1 }
                    : item
            )
            .filter(item => item.quantity > 0)
        );
    };

    const removeFromCart = (productId) => {
        setCartItems(cartItems.filter(item => item.id !== productId));
    };

    const clearCart = () => {
        setCartItems([]);
        setShowCheckout(false);
    };

    const openCheckout = () => {
        if (cartItems.length === 0) {
            alert('Your cart is empty.');
            return;
        }

        setShowCheckout(true);
    };

    const placeOrder = async () => {
        if (!user) {
            alert('You must be logged in to place an order.');
            return;
        }

        if (cartItems.length === 0) {
            alert('Your cart is empty.');
            return;
        }

        try {
            const paymentData = {
                items: cartItems.map(item => ({
                    productId: item.id,
                    quantity: item.quantity
                }))
            };

            const response = await axios.post(
                `${apiBaseUrl}/Payments/create-checkout-session`,
                paymentData,
                getAuthConfig()
            );

            window.location.href = response.data.url;
        } catch (err) {
            console.error(err);
            alert('Payment could not be started.');
        }
    };

    const toggleWishlist = async (product) => {
        if (!user) {
            alert('You must be logged in to use wishlist.');
            setShowAuth(true);
            return;
        }

        if (user.role === 'Admin' || user.role === 'Employee') {
            alert('Wishlist is available for customers.');
            return;
        }

        const exists = wishlistItems.some(item => item.id === product.id);

        try {
            if (exists) {
                await axios.delete(`${apiBaseUrl}/Wishlist/${product.id}`, getAuthConfig());
            } else {
                await axios.post(`${apiBaseUrl}/Wishlist/${product.id}`, {}, getAuthConfig());
            }

            await loadWishlist();
        } catch (err) {
            console.error(err);
            alert('Wishlist action failed.');
        }
    };

    const isInWishlist = (productId) => {
        return wishlistItems.some(item => item.id === productId);
    };

    const cartCount = cartItems.reduce((sum, item) => sum + item.quantity, 0);
    const cartTotal = cartItems.reduce((sum, item) => sum + item.price * item.quantity, 0);

    if (!user && showAuth) {
        return (
            <div className="auth-page">
                <div className="auth-card">
                    <div className="brand-logo">LUMÉA BEAUTY</div>
                    <h1>{authMode === 'login' ? 'Welcome back' : 'Create account'}</h1>
                    <p>
                        {authMode === 'login'
                            ? 'Log in to continue shopping.'
                            : 'Create your account and start your beauty routine.'}
                    </p>

                    <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} />

                    {authMode === 'register' && (
                        <>
                            <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
                            <input type="text" placeholder="Full name" value={fullName} onChange={(e) => setFullName(e.target.value)} />
                            <input type="text" placeholder="Address" value={address} onChange={(e) => setAddress(e.target.value)} />
                            <input type="text" placeholder="Phone number" value={phoneNumber} onChange={(e) => setPhoneNumber(e.target.value)} />
                        </>
                    )}

                    <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />

                    <button className="auth-main-button" onClick={authMode === 'login' ? handleLogin : handleRegister}>
                        {authMode === 'login' ? 'Log in' : 'Register'}
                    </button>

                    <button
                        className="auth-switch-button"
                        onClick={() => {
                            setAuthMode(authMode === 'login' ? 'register' : 'login');
                            setPassword('');
                        }}
                    >
                        {authMode === 'login' ? 'No account? Register' : 'Already have an account? Log in'}
                    </button>

                    <button
                        className="auth-switch-button"
                        onClick={() => setShowAuth(false)}
                    >
                        Continue as guest
                    </button>
                </div>
            </div>
        );
    }

    return (
        <div className="page">
            <header className="hero">
                <div className="top-bar">
                    <div>
                        <div className="brand-logo">LUMÉA BEAUTY</div>
                        <p className="user-info">
                            {user ? <>Logged in as <strong>{user.username}</strong> ({user.role})</> : <>Browsing as <strong>Guest</strong></>}
                        </p>
                    </div>

                    <div className="top-actions">
                        {user && user.role !== 'Admin' && user.role !== 'Employee' && (
                            <button
                                className="orders-button"
                                onClick={() => {
                                    loadOrders();
                                    setShowOrders(!showOrders);
                                }}
                            >
                                My Orders
                            </button>
                        )}

                        {(user?.role === 'Admin' || user?.role === 'Employee') && (
                            <>
                                <button
                                    className="orders-button"
                                    onClick={() => {
                                        loadAdminOrders();
                                        setShowAdminOrders(!showAdminOrders);
                                    }}
                                >
                                    Manage Orders
                                </button>

                                <button
                                    className="orders-button"
                                    onClick={() => {
                                        loadPaymentTransactions();
                                        setShowPaymentTransactions(!showPaymentTransactions);
                                    }}
                                >
                                    Payments
                                </button>
                            </>
                        )}

                        {user?.role === 'Admin' && (
                            <button className="admin-button" onClick={() => setShowAdd(true)}>
                                + Add Product
                            </button>
                        )}

                        {!user && (
                            <button className="admin-button" onClick={() => setShowAuth(true)}>
                                Login / Register
                            </button>
                        )}

                        {user && (
                            <button className="logout-button" onClick={handleLogout}>
                                Logout
                            </button>
                        )}
                    </div>
                </div>

                <section className="hero-card">
                    <div className="hero-content">
                        <span className="hero-label">Premium beauty webshop</span>

                        <h1>Discover your perfect beauty routine.</h1>

                        <p>
                            Explore skincare, makeup, fragrance and care essentials carefully selected
                            for modern online shopping.
                        </p>

                        <div className="hero-buttons">
                            <button
                                onClick={() =>
                                    document.querySelector('.product-grid')?.scrollIntoView({
                                        behavior: 'smooth'
                                    })
                                }
                            >
                                Shop now
                            </button>

                            <button
                                className="secondary-hero-button"
                                onClick={() =>
                                    document.querySelector('.category-section')?.scrollIntoView({
                                        behavior: 'smooth'
                                    })
                                }
                            >
                                View categories
                            </button>
                        </div>

                        <div className="hero-stats">
                            <div>
                                <strong>20+</strong>
                                <span>Products</span>
                            </div>

                            <div>
                                <strong>5</strong>
                                <span>Categories</span>
                            </div>

                            <div>
                                <strong>24/7</strong>
                                <span>Online shop</span>
                            </div>
                        </div>
                    </div>

                    <div className="hero-visual">
                        <div className="hero-product hero-product-main">
                            <img
                                src="https://www.spacenk.com/on/demandware.static/-/Sites-spacenkmastercatalog/default/dw95036ea5/products/RARE/UK200048738_RARE.jpg"
                                alt="Beauty product"
                            />
                        </div>

                        <div className="hero-product hero-product-small top">
                            <img
                                src="https://static.beautytocare.com/cdn-cgi/image/f=auto/media/catalog/product/c/e/cerave-moisturizing-cream-dry-to-very-dry-skin-170g_3.jpg"
                                alt="Skincare product"
                            />
                        </div>

                        <div className="hero-product hero-product-small bottom">
                            <img
                                src="https://apothecary.rs/18361-large_default/dior-lip-glow-oil-n001-pink-6ml.jpg"
                                alt="Lip oil"
                            />
                        </div>
                    </div>
                </section>
            </header>

            {showAdminOrders && (user?.role === 'Admin' || user?.role === 'Employee') && (
                <section className="orders-section">
                    <h2>Manage Orders</h2>
                    <p className="section-note">Here staff can see customer orders and update their status.</p>

                    {adminOrders.length === 0 ? (
                        <p className="orders-empty">No orders yet.</p>
                    ) : (
                        <div className="orders-list">
                            {adminOrders.map(order => (
                                <div key={order.id} className="order-card">
                                    <div className="order-header">
                                        <div>
                                            <h3>Order #{order.id}</h3>
                                            <p>Customer: {order.user?.username || `User ${order.userId}`}</p>
                                            <p>Email: {order.user?.email || 'Not provided'}</p>
                                            <p>Phone: {order.phoneNumber || 'Not provided'}</p>
                                            <p>Address: {order.shippingAddress || 'Not provided'}</p>
                                            <p>Payment: {order.paymentStatus || 'Pending'}</p>
                                            <p>{new Date(order.orderDate).toLocaleString()}</p>
                                        </div>

                                        <strong>{order.totalAmount.toLocaleString()} RSD</strong>
                                    </div>

                                    <span className="order-status">{order.status}</span>

                                    <select
                                        className="status-select"
                                        value={order.status}
                                        onChange={(e) => updateOrderStatus(order.id, e.target.value)}
                                    >
                                        <option value="Created">Created</option>
                                        <option value="Processing">Processing</option>
                                        <option value="Shipped">Shipped</option>
                                        <option value="Completed">Completed</option>
                                        <option value="Cancelled">Cancelled</option>
                                    </select>

                                    <div className="order-items">
                                        {order.orderItems?.map(item => (
                                            <div key={item.id} className="order-item">
                                                <span>{item.product?.name || `Product ${item.productId}`}</span>
                                                <small>{item.quantity} × {item.unitPrice.toLocaleString()} RSD</small>
                                            </div>
                                        ))}
                                    </div>
                                </div>
                            ))}
                        </div>
                    )}
                </section>
            )}

            {showPaymentTransactions && (user?.role === 'Admin' || user?.role === 'Employee') && (
                <section className="orders-section">
                    <h2>Payment Transactions</h2>
                    <p className="section-note">Here staff can review Stripe payment transactions.</p>

                    {paymentTransactions.length === 0 ? (
                        <p className="orders-empty">No payment transactions yet.</p>
                    ) : (
                        <div className="orders-list">
                            {paymentTransactions.map(transaction => (
                                <div key={transaction.id} className="order-card">
                                    <div className="order-header">
                                        <div>
                                            <h3>Transaction #{transaction.id}</h3>
                                            <p>Customer: {transaction.user?.username || `User ${transaction.userId}`}</p>
                                            <p>Status: {transaction.status}</p>
                                            <p>{new Date(transaction.createdAt).toLocaleString()}</p>
                                        </div>

                                        <strong>{transaction.amount.toLocaleString()} {transaction.currency?.toUpperCase()}</strong>
                                    </div>

                                    <span className="order-status">{transaction.status}</span>

                                    <div className="order-items">
                                        <div className="order-item">
                                            <span>Stripe Session</span>
                                            <small>{transaction.stripeSessionId}</small>
                                        </div>

                                        <div className="order-item">
                                            <span>Payment Intent</span>
                                            <small>{transaction.paymentIntentId || 'Not confirmed yet'}</small>
                                        </div>

                                        <div className="order-item">
                                            <span>Order ID</span>
                                            <small>{transaction.orderId || 'Not created yet'}</small>
                                        </div>
                                    </div>
                                </div>
                            ))}
                        </div>
                    )}
                </section>
            )}

            {showOrders && user && user.role !== 'Admin' && user.role !== 'Employee' && (
                <section className="orders-section">
                    <h2>My Orders</h2>
                    <p className="section-note">Here customer can see personal order history.</p>

                    {orders.length === 0 ? (
                        <p className="orders-empty">You have no orders yet.</p>
                    ) : (
                        <div className="orders-list">
                            {orders.map(order => (
                                <div key={order.id} className="order-card">
                                    <div className="order-header">
                                        <div>
                                            <h3>Order #{order.id}</h3>
                                            <p>{new Date(order.orderDate).toLocaleString()}</p>
                                            <p>Payment: {order.paymentStatus || 'Pending'}</p>
                                            <p>Phone: {order.phoneNumber || 'Not provided'}</p>
                                            <p>Address: {order.shippingAddress || 'Not provided'}</p>
                                        </div>

                                        <strong>{order.totalAmount.toLocaleString()} RSD</strong>
                                    </div>

                                    <span className="order-status">{order.status}</span>

                                    <div className="order-items">
                                        {order.orderItems?.map(item => (
                                            <div key={item.id} className="order-item">
                                                <span>{item.product?.name || `Product ${item.productId}`}</span>
                                                <small>{item.quantity} × {item.unitPrice.toLocaleString()} RSD</small>
                                            </div>
                                        ))}
                                    </div>
                                </div>
                            ))}
                        </div>
                    )}
                </section>
            )}

            <section className="cart-summary">
                <div>
                    <h2>Your cart</h2>
                    <p>{cartCount} item(s) selected</p>
                </div>

                <strong>{cartTotal.toLocaleString()} RSD</strong>
            </section>

            {cartItems.length > 0 && (
                <section className="cart-box">
                    {cartItems.map(item => (
                        <div key={item.id} className="cart-item">
                            <img src={item.imageUrl} alt={item.name} />

                            <div className="cart-item-info">
                                <span>{item.brand}</span>
                                <h3>{item.name}</h3>
                                <p>{item.price.toLocaleString()} RSD</p>
                            </div>

                            <div className="quantity-controls">
                                <button onClick={() => decreaseQuantity(item.id)}>-</button>
                                <span>{item.quantity}</span>
                                <button onClick={() => increaseQuantity(item.id)}>+</button>
                            </div>

                            <strong>{(item.price * item.quantity).toLocaleString()} RSD</strong>

                            <button className="remove-button" onClick={() => removeFromCart(item.id)}>
                                Remove
                            </button>
                        </div>
                    ))}

                    <button className="checkout-button" onClick={openCheckout}>
                        Checkout
                    </button>

                    <button className="clear-cart-button" onClick={clearCart}>
                        Clear cart
                    </button>
                </section>
            )}

            <section className="wishlist-section">
                <h2>Wishlist</h2>
                <p>{wishlistItems.length} saved product(s)</p>

                {wishlistItems.length > 0 && (
                    <div className="wishlist-list">
                        {wishlistItems.map(item => (
                            <div key={item.id} className="wishlist-item">
                                <img src={item.imageUrl} alt={item.name} />
                                <div>
                                    <strong>{item.name}</strong>
                                    <span>{item.brand}</span>
                                </div>
                                <button onClick={() => toggleWishlist(item)}>Remove</button>
                            </div>
                        ))}
                    </div>
                )}
            </section>

            <section className="category-section">
                <h2>Shop by category</h2>

                <div className="category-list">
                    <button
                        className={categoryId === '' ? 'active-category' : ''}
                        onClick={() => {
                            setCategoryId('');
                            setPageNumber(1);
                        }}
                    >
                        All products
                    </button>

                    {categories.map(category => (
                        <button
                            key={category.id}
                            className={categoryId == category.id ? 'active-category' : ''}
                            onClick={() => {
                                setCategoryId(category.id);
                                setPageNumber(1);
                            }}
                        >
                            {category.name}
                        </button>
                    ))}
                </div>
            </section>

            <section className="filters">
                <input
                    type="text"
                    placeholder="Search products..."
                    value={search}
                    onChange={(e) => {
                        setSearch(e.target.value);
                        setPageNumber(1);
                    }}
                />

                <select
                    value={brand}
                    onChange={(e) => {
                        setBrand(e.target.value);
                        setPageNumber(1);
                    }}
                >
                    <option value="">All brands</option>
                    {brands.map(item => (
                        <option key={item} value={item}>
                            {item}
                        </option>
                    ))}
                </select>

                <select value={sortBy} onChange={(e) => { setSortBy(e.target.value); setPageNumber(1); }}>
                    <option value="">Recommended</option>
                    <option value="price_asc">Price: Low to High</option>
                    <option value="price_desc">Price: High to Low</option>
                    <option value="name_asc">Name A-Z</option>
                    <option value="name_desc">Name Z-A</option>
                    <option value="brand_asc">Brand A-Z</option>
                    <option value="brand_desc">Brand Z-A</option>
                </select>

                <button onClick={resetFilters}>Reset</button>
            </section>

            {error && <p className="error">{error}</p>}
            {loading && <p className="loading">Loading products...</p>}

            <section className="product-grid">
                {products.map(product => (
                    <article key={product.id} className="product-card">
                        <button className={isInWishlist(product.id) ? 'wishlist-button active-heart' : 'wishlist-button'} onClick={() => toggleWishlist(product)}>
                            ♥
                        </button>

                        <div className="image-container">
                            <img src={product.imageUrl} alt={product.name} />
                        </div>

                        <div className="product-info">
                            <span className="brand-name">{product.brand}</span>
                            <h3>{product.name}</h3>
                            <p className="description">{product.description}</p>
                            <span className="category-name">{product.category?.name}</span>

                            <div className="product-bottom">
                                <strong>{product.price.toLocaleString()} RSD</strong>
                                <small>{product.stockQuantity} in stock</small>
                            </div>

                            <button className="buy-button" onClick={() => addToCart(product)}>
                                Add to cart
                            </button>

                            {user?.role === 'Admin' && (
                                <div className="admin-product-actions">
                                    <button onClick={() => openEditProduct(product)}>
                                        Edit
                                    </button>

                                    <button className="delete-product-button" onClick={() => deleteProduct(product.id)}>
                                        Delete
                                    </button>
                                </div>
                            )}
                        </div>
                    </article>
                ))}
            </section>

            <div className="pagination">
                <button disabled={pageNumber === 1} onClick={() => setPageNumber(pageNumber - 1)}>
                    Previous
                </button>

                <span>Page {pageNumber} of {totalPages}</span>

                <button disabled={pageNumber >= totalPages} onClick={() => setPageNumber(pageNumber + 1)}>
                    Next
                </button>

            </div>

            {showCheckout && (
                <div className="modal-overlay" onClick={() => setShowCheckout(false)}>
                    <div className="checkout-modal" onClick={(e) => e.stopPropagation()}>
                        <h2>Review your order</h2>
                        <p className="checkout-subtitle">Please check your items before continuing to payment.</p>

                        <div className="checkout-items">
                            {cartItems.map(item => (
                                <div key={item.id} className="checkout-item">
                                    <img src={item.imageUrl} alt={item.name} />
                                    <div>
                                        <strong>{item.name}</strong>
                                        <span>{item.brand}</span>
                                    </div>
                                    <p>{item.quantity} × {item.price.toLocaleString()} RSD</p>
                                </div>
                            ))}
                        </div>

                        <div className="checkout-total">
                            <span>Total</span>
                            <strong>{cartTotal.toLocaleString()} RSD</strong>
                        </div>

                        <button className="place-order-button" onClick={placeOrder}>
                            Continue to payment
                        </button>

                        <button className="cancel-button" onClick={() => setShowCheckout(false)}>
                            Continue shopping
                        </button>
                    </div>
                </div>
            )}

            {showAdd && (
                <div className="modal-overlay" onClick={() => setShowAdd(false)}>
                    <div className="add-product-modal" onClick={(e) => e.stopPropagation()}>
                        <h2>Add new product</h2>

                        <input placeholder="Product name" value={newProduct.name} onChange={(e) => setNewProduct({ ...newProduct, name: e.target.value })} />
                        <input placeholder="Brand" value={newProduct.brand} onChange={(e) => setNewProduct({ ...newProduct, brand: e.target.value })} />
                        <input placeholder="Description" value={newProduct.description} onChange={(e) => setNewProduct({ ...newProduct, description: e.target.value })} />
                        <input placeholder="Price" type="number" value={newProduct.price} onChange={(e) => setNewProduct({ ...newProduct, price: e.target.value })} />
                        <input placeholder="Stock quantity" type="number" value={newProduct.stockQuantity} onChange={(e) => setNewProduct({ ...newProduct, stockQuantity: e.target.value })} />
                        <input placeholder="Image URL" value={newProduct.imageUrl} onChange={(e) => setNewProduct({ ...newProduct, imageUrl: e.target.value })} />

                        <select value={newProduct.categoryId} onChange={(e) => setNewProduct({ ...newProduct, categoryId: e.target.value })}>
                            <option value="">Select category</option>
                            {categories.map(category => (
                                <option key={category.id} value={category.id}>
                                    {category.name}
                                </option>
                            ))}
                        </select>

                        <button className="save-product-button" onClick={handleAddProduct}>
                            Save product
                        </button>

                        <button className="cancel-button" onClick={() => setShowAdd(false)}>
                            Cancel
                        </button>
                    </div>
                </div>
            )}

            {showEdit && editingProduct && (
                <div className="modal-overlay" onClick={() => setShowEdit(false)}>
                    <div className="add-product-modal" onClick={(e) => e.stopPropagation()}>
                        <h2>Edit product</h2>

                        <input placeholder="Product name" value={editingProduct.name} onChange={(e) => setEditingProduct({ ...editingProduct, name: e.target.value })} />
                        <input placeholder="Brand" value={editingProduct.brand} onChange={(e) => setEditingProduct({ ...editingProduct, brand: e.target.value })} />
                        <input placeholder="Description" value={editingProduct.description} onChange={(e) => setEditingProduct({ ...editingProduct, description: e.target.value })} />
                        <input placeholder="Price" type="number" value={editingProduct.price} onChange={(e) => setEditingProduct({ ...editingProduct, price: e.target.value })} />
                        <input placeholder="Stock quantity" type="number" value={editingProduct.stockQuantity} onChange={(e) => setEditingProduct({ ...editingProduct, stockQuantity: e.target.value })} />
                        <input placeholder="Image URL" value={editingProduct.imageUrl} onChange={(e) => setEditingProduct({ ...editingProduct, imageUrl: e.target.value })} />

                        <select value={editingProduct.categoryId} onChange={(e) => setEditingProduct({ ...editingProduct, categoryId: e.target.value })}>
                            <option value="">Select category</option>
                            {categories.map(category => (
                                <option key={category.id} value={category.id}>
                                    {category.name}
                                </option>
                            ))}
                        </select>

                        <button className="save-product-button" onClick={handleEditProduct}>
                            Save changes
                        </button>

                        <button
                            className="cancel-button"
                            onClick={() => {
                                setShowEdit(false);
                                setEditingProduct(null);
                            }}
                        >
                            Cancel
                        </button>
                    </div>
                </div>
            )}
        </div>
    );
}

export default App;